using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
    public enum TurnInfo {
        PLAYER,
        ENEMY
    }

    [SerializeField] private PlayerControl _playerControl = null;
    public PlayerControl PlayerCtrl { get { return _playerControl; }}
    [SerializeField] private MonsterControl _enemyControl = null;
    public MonsterControl EnemyCtrl { get { return _enemyControl; } }
    [SerializeField] Camera _cardCamera = null;
    public BattleEntity SelectedTarget { get; private set; }
    private TurnInfo _currentTurn;
    private int _turnCount = 0;
    private int _currentStage;
    private float _touchTimeAgo;
    private ObserverSubject<BattleUIArgs> _onSkillUsedEvent;
    private int _selectedSkillIndex = -1;

    private void Start() {
        SkillManager.GetInstance().Initialize(_cardCamera);

        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        _enemyControl.Initialize();
        
        _onSkillUsedEvent = new ObserverSubject<BattleUIArgs>();
        _onSkillUsedEvent.Subscribe_And_Listen_CurrentData += GetComponent<BattleUI>().OnSkillUsed;

        StartTurn();
        SetupUI();
    }

    private void Update() {
        _playerControl.Progress();
        _enemyControl.Progress();

        if (_currentTurn == TurnInfo.ENEMY) return;

        SkillState state = SkillManager.GetInstance().State;
        if (state == SkillState.NOTHING) return;

        Vector2 touchPosition = Utility.GetTouchPosition(_cardCamera);
        var hand = _playerControl.SkillsInHand;
        int handCounts = hand.Count;
        bool canSelectTarget = false;
        int usedSkillIndex = -1;

        if (_selectedSkillIndex == -1) {
            for (int i = 0; i < handCounts; ++i) {
                bool isCostEnough = _playerControl.CanUseSkill(hand[i].GetRequireCost());

                if (ListenTouchMoveInput(touchPosition, hand[i], isCostEnough)) {
                    //DeSelectAllSkills(handCounts, i);
                    _selectedSkillIndex = i;
                    break;
                }
            }
        }
        else {
            Skill skill = hand[_selectedSkillIndex];
            skill.Progress(touchPosition);
            canSelectTarget = skill.CanSelectTarget();
            bool isCostEnough = _playerControl.CanUseSkill(skill.GetRequireCost());
            if (isCostEnough) {
                usedSkillIndex = ListenTouchUpInput(touchPosition, skill);
            }
        }

        if (usedSkillIndex != -1) {
            if (canSelectTarget) {
                SetSkillTarget(hand[usedSkillIndex], touchPosition);
                if (!SelectedTarget) return;
            }
            _playerControl.UseSkill(hand[usedSkillIndex], this);

            SetupUI();

            SkillManager.GetInstance().ReturnCard(hand[usedSkillIndex]);
            hand.RemoveAt(usedSkillIndex);
            SkillManager.GetInstance().AlignCard(hand);
        }
    }

    public void StartTurn() {
        ++_turnCount;

        if (_currentTurn == TurnInfo.PLAYER) {
            _playerControl.RefreshCost();
            _playerControl.DrawCard(true);
            SkillManager.GetInstance().State = SkillState.CARD_DRAG;
        }
        else {
            StartCoroutine(StartEnemyTurn());
        }
    }

    private IEnumerator StartEnemyTurn() {
        yield return _enemyControl.UseSkill(this);
        EndTurn();
    }

    public void OnTurnEndButtonClicked() {
        if (_currentTurn == TurnInfo.ENEMY) return;
        EndTurn();
    }

    public void EndTurn() {
        int curTurn = (int)_currentTurn;
        int nextTurn = (curTurn + 1) % 2;
        _currentTurn = (TurnInfo)nextTurn;
        SkillManager.GetInstance().State = SkillState.CARD_OVER;

        StartTurn();
    }

    private void SetSkillTarget(Skill skill, Vector2 touchPosition) {
        
        if (skill.IsCharacterTarget()) {
            SelectedTarget = _playerControl.GetSelectedCharacter(touchPosition);
        }
        else {
            SelectedTarget = _enemyControl.GetSelectedEnemy(touchPosition);
        }
    }

    private bool ListenTouchMoveInput(Vector2 curTouchPos, Skill skill, bool isCostEnough) {
        bool isSelected = false;
        #if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            isSelected = skill.OnTouchMoved(curTouchPos, isCostEnough);
        }
        /*#else
        if (Input.touchCount > 0) {
            switch (touch.phase) {
            case TouchPhase.Moved:
                skill.OnTouchMoved(curTouchPos);
                break;
            case TouchPhase.Ended:
                canUseSkill = skill.OnTouchUp();
                _aimTransform.gameObject.SetActive(false);
                break;
            }
        }*/
        #endif
        return isSelected;
    }

    private int ListenTouchUpInput(Vector2 curTouchPos, Skill skill) {
        int usedSkillIndex = -1;
        if (Input.GetMouseButtonUp(0)) {
            if (skill.OnTouchUp()) {
                usedSkillIndex = _selectedSkillIndex;
            }
            _selectedSkillIndex = -1;
        }
        return usedSkillIndex;
    }

    private void SetupUI() {
        float[] ally01 = _playerControl.GetFillAmounts();
        float[] enemy01 = _enemyControl.GetFillAmounts();
        int curCost = _playerControl.CurrentCost;
        int maxCost = _playerControl.GetMaxCost();
        _onSkillUsedEvent.DoNotify(new BattleUIArgs(ally01, enemy01, curCost, maxCost));
    }

    private void DeSelectAllSkills(int handCounts, int ignoreIndex) {
        for (int i = 0; i < handCounts; ++i) {
            if (i == ignoreIndex) continue;
            SkillManager.GetInstance().EnlargeCard(false, _playerControl.SkillsInHand[i]);
        }
    }
}
