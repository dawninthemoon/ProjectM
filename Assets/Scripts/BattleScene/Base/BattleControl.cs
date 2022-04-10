using System.Collections;
using UnityEngine;

public class BattleControl : MonoBehaviour
{
    public enum TurnInfo
    {
        PLAYER,
        ENEMY
    }

    [SerializeField] private PlayerControl _playerControl = null;

    public PlayerControl PlayerCtrl
    { get { return _playerControl; } }

    [SerializeField] private MonsterControl _enemyControl = null;

    public MonsterControl EnemyCtrl
    { get { return _enemyControl; } }

    public BattleEntity SelectedTarget { get; private set; }
    private TurnInfo _currentTurn;
    private int _turnCount = 0;
    private int _currentStage;
    private float _touchTimeAgo;
    private ObserverSubject<BattleUIArgs> _onSkillUsedEvent;
    private int _selectedSkillIndex = -1;

    private void Start()
    {
        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        _enemyControl.Initialize();

        _onSkillUsedEvent = new ObserverSubject<BattleUIArgs>();
        _onSkillUsedEvent.Subscribe_And_Listen_CurrentData += GetComponent<BattleUI>().OnSkillUsed;

        StartTurn();
        SetupUI();
    }

    private void Update()
    {
        _playerControl.Progress();
        _enemyControl.Progress();

        if (_currentTurn == TurnInfo.ENEMY || _playerControl.IsDefeated()) return;

        var hand = _playerControl.SkillsInHand;
        int handCounts = hand.Count;

    }

    private void LateUpdate()
    {
        _playerControl.LateProgress();
        _enemyControl.LateProgress();
    }

    public void StartTurn()
    {
        ++_turnCount;

        if (_currentTurn == TurnInfo.PLAYER)
        {
            if (_playerControl.IsDefeated()) return;

            _playerControl.RefreshCost();
            _playerControl.DrawCard(true);
        }
        else
        {
            StartCoroutine(StartEnemyTurn());
        }
    }

    private IEnumerator StartEnemyTurn()
    {
        yield return _enemyControl.UseSkill(this, SetupUI);
        EndTurn();
    }

    public void OnTurnEndButtonClicked()
    {
        if (_currentTurn == TurnInfo.ENEMY) return;
        EndTurn();
    }

    public void EndTurn()
    {
        int curTurn = (int)_currentTurn;
        int nextTurn = (curTurn + 1) % 2;
        _currentTurn = (TurnInfo)nextTurn;
        SkillManager.Instance.State = SkillState.CARD_OVER;

        StartTurn();
    }

    private void SetSkillTarget(Skill skill, Vector2 touchPosition)
    {
        EntityControl entityControl = skill.IsAllyTarget() ? (_playerControl as EntityControl) : (_enemyControl as EntityControl);
        SelectedTarget = entityControl.GetSelectedEntity(touchPosition);
    }

    private bool ListenTouchMoveInput(Vector2 curTouchPos, Skill skill, bool isCostEnough)
    {
        bool isSelected = false;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
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

    private int ListenTouchUpInput(Vector2 curTouchPos, Skill skill)
    {
        int usedSkillIndex = -1;
        if (Input.GetMouseButtonUp(0))
        {
            if (skill.OnTouchUp())
            {
                usedSkillIndex = _selectedSkillIndex;
            }
            _selectedSkillIndex = -1;
        }
        return usedSkillIndex;
    }

    private void SetupUI()
    {
        float[] ally01 = _playerControl.GetFillAmounts();
        float[] enemy01 = _enemyControl.GetFillAmounts();
        int curCost = _playerControl.CurrentCost;
        int maxCost = _playerControl.GetMaxCost();
        _onSkillUsedEvent.DoNotify(new BattleUIArgs(ally01, enemy01, curCost, maxCost));
    }
}