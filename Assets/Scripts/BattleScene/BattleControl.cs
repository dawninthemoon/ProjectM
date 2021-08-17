using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
    public enum TurnInfo {
        PLAYER,
        ENEMY
    }

    [SerializeField] PlayerControl _playerControl = null;
    [SerializeField] EnemyControl _enemyControl = null;
    public Entity SelectedTarget { get; private set; }
    private static readonly float LongTouchTime = 1.5f;
    private TurnInfo _currentTurn;
    private int _turnCount;
    private int _currentStage;
    private float _touchTimeAgo;
    private ObserverSubject<BattleUIArgs> _onSkillUsedEvent;

    private void Start() {
        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        _enemyControl.Initialize();
        
        _onSkillUsedEvent = new ObserverSubject<BattleUIArgs>();
        _onSkillUsedEvent.Subscribe_And_Listen_CurrentData += GetComponent<BattleUI>().OnSkillUsed;

        StartTurn();
        SetupUI();
    }

    private void Update() {
        SkillState state = SkillManager.GetInstance().State;
        if (state == SkillState.NOTHING) return;

        Vector2 touchPosition = Utility.GetTouchPosition();
        var hand = _playerControl.SkillsInHand;
        int handCounts = hand.Count;
        bool canSelectTarget = false;
        int usedSkillIndex = -1;

        for (int i = 0; i < handCounts; ++i) {
            if (ListenInput(touchPosition, hand[i])) {
                usedSkillIndex = i;
            }
            hand[i].Progress(touchPosition);
            if (hand[i].IsTouching || (usedSkillIndex != -1)) {
                canSelectTarget = hand[i].CanSelectTarget();
                break;
            }
        }

        if (canSelectTarget) {
            SkillManager.GetInstance().SetAimPosition(touchPosition);
        }
        if (usedSkillIndex != -1) {
            if (canSelectTarget) {
                SelectedTarget = _enemyControl.GetSelectedEnemy(touchPosition);
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
        _playerControl.RefreshCost();
        _playerControl.DrawCard(true);
        SkillManager.GetInstance().State = SkillState.CARD_DRAG;
    }

    public void EndTurn() {
        int curTurn = (int)_currentTurn;
        int nextTurn = (curTurn + 1) % 2;
        _currentTurn = (TurnInfo)nextTurn;
        SkillManager.GetInstance().State = SkillState.CARD_OVER;
    }

    private bool ListenInput(Vector2 curTouchPos, Skill skill) {
        bool canUseSkill = false;
        #if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            skill.OnTouchMoved(curTouchPos);
        }
        else if (Input.GetMouseButtonUp(0)) {
            canUseSkill = skill.OnTouchUp();
        }
        #else
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
        }
        #endif
        return canUseSkill;
    }

    private void SetupUI() {
        float[] ally01 = _playerControl.GetFillAmounts();
        float[] enemy01 = _enemyControl.GetFillAmounts();
        int curCost = _playerControl.CurrentCost;
        int maxCost = _playerControl.GetMaxCost();
        _onSkillUsedEvent.DoNotify(new BattleUIArgs(ally01, enemy01, curCost, maxCost));
    }
}
