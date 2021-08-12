using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
    public enum TurnInfo {
        PLAYER,
        ENEMY
    }

    [SerializeField] PlayerControl _playerControl = null;
    TurnInfo _currentTurn;
    int _turnCount;
    int _currentStage;

    void Start() {
        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        StartTurn();
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
}