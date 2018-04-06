using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnBasedBattle : MonoBehaviour {

    enum BattleState
    {
        START,
        FIGHT,
        BAG,
        RADEMON,
        RUN
    }

    private BattleState currentState;
    public Button Move1;
    public Button Move2;
    public Button Move3;
    public Button Move4;

    // Use this for initialization
    void Start () {
        currentState = BattleState.START;
        DisableMoveButtons();
    }

    void DisableMoveButtons()
    {
        Move1.interactable = false;
        Move2.interactable = false;
        Move3.interactable = false;
        Move4.interactable = false;
    }
    
    void EnableMoveButtons()
    {
        Move1.interactable = true;
        Move2.interactable = true;
        Move3.interactable = true;
        Move4.interactable = true;
    }

    public void EnterFightMode()
    {
        currentState = BattleState.FIGHT;
        EnableMoveButtons();
    }

    public void EnterBagMode()
    {
        DisableMoveButtons();
        currentState = BattleState.BAG;
    }

    public void EnterRademonMode()
    {
        DisableMoveButtons();
        currentState = BattleState.RADEMON;
    }

    public void EnterRunMode()
    {
        DisableMoveButtons();
        currentState = BattleState.RUN;
        SceneManager.LoadScene("MainScene");
    }
}
