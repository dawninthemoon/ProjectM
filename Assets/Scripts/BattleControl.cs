using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
    public enum TurnInfo {
        PLAYER,
        ENEMY
    }

    [SerializeField] PlayerControl _playerControl = null;
    [SerializeField] Transform _aimTransform = null;
    static readonly float LongTouchTime = 1.5f;
    TurnInfo _currentTurn;
    int _turnCount;
    int _currentStage;
    float _touchTimeAgo;

    void Start() {
        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        StartTurn();
    }

    void Update() {
        SkillState state = SkillManager.GetInstance().State;
        if (state == SkillState.NOTHING) return;

        var hand = _playerControl.SkillsInHand;
        int handCounts = hand.Count;
        bool canSelectTarget = false;
        Vector2 curTouchPos = Utility.GetTouchPosition();
        for (int i = 0; i < handCounts; ++i) {
            ListenInput(curTouchPos, hand[i]);
            canSelectTarget = hand[i].Progress(curTouchPos);
            if (hand[i].IsTouching)
                break;
        }

        if (canSelectTarget) {
            _aimTransform.gameObject.SetActive(true);
            _aimTransform.transform.position = curTouchPos;
        }
    }

    public void StartTurn() {
        ++_turnCount;
        _playerControl.CurrentCost = 3;
        _playerControl.DrawCard(true);
        SkillManager.GetInstance().State = SkillState.CARD_DRAG;
    }

    public void EndTurn() {
        int curTurn = (int)_currentTurn;
        int nextTurn = (curTurn + 1) % 2;
        _currentTurn = (TurnInfo)nextTurn;
        SkillManager.GetInstance().State = SkillState.CARD_OVER;
    }

    void ListenInput(Vector2 curTouchPos, Skill skill) {
        #if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            skill.OnTouchMoved(curTouchPos);
        }
        else if (Input.GetMouseButtonUp(0)) {
            skill.OnTouchUp();
            _aimTransform.gameObject.SetActive(false);
        }
        #else
        if (Input.touchCount > 0) {
            switch (touch.phase) {
            case TouchPhase.Moved:
                skill.OnTouchMoved(curTouchPos);
                break;
            case TouchPhase.Ended:
                skill.OnTouchUp();
                _aimTransform.gameObject.SetActive(false);
                break;
            }
        }
        #endif
    }
}
