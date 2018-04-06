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
        ITEMS,
        WEAPONS,
        RUN
    }

    private BattleState currentState;
    private Moves currentMove;
    public Button btn_Move1;
    public Button btn_Move2;
    public Button btn_Move3;
    public Button btn_Move4;
    private bool PlayerTurn;
    private Stats PlayerStats;
    private Stats EnemyStats;
    private Moves LPUNCH; //light punch
    private Moves HPUNCH; //heavy punch
    private Moves LKICK; //light kick
    private Moves HKICK; //heavy kick
    public Button[] PlayerButtons;

    // Use this for initialization
    void Start () {
        PlayerStats = new Stats(100, false);
        EnemyStats = new Stats(50, false);
        currentState = BattleState.START;
        DisableMoveButtons();
        InitializeMoves();
        PlayerTurn = true;
        EnablePlayerButtons();
    }

    void DisablePlayerButtons()
    {
        foreach (Button b in PlayerButtons)
        {
            b.interactable = false;
        }
    }

    void EnablePlayerButtons()
    {
        foreach (Button b in PlayerButtons)
        {
            b.interactable = true;
        }
    }

    void InitializeMoves()
    {
        LPUNCH = new Moves(10, 5);
        HPUNCH = new Moves(20, 2);
        LKICK = new Moves(15, 5);
        HKICK = new Moves(25, 2);
    }

    void TurnChecker()
    {
        if (PlayerTurn == true)
        {
            EnablePlayerButtons();
            while ((PlayerStats.IsDead == false) && (currentState != BattleState.RUN))
            {
                if (currentState == BattleState.ITEMS)
                {
                    //Open Item inventory
                }
                else if (currentState == BattleState.WEAPONS)
                {
                    //Check Available Weapons
                }
                else if (currentState == BattleState.FIGHT)
                {
                    //Change Enemy Stats
                }
                else if (currentState == BattleState.RUN)
                {
                    //Message + Audio
                    SceneManager.LoadScene("MainScene");
                    break;
                }
            }
            PlayerTurn = false;
        }
        else
        {
            //Random Enemy Attack
        }

    }

    public void CheckMoveClicked(Button move)
    {
        if (move = btn_Move1)
            currentMove = LPUNCH;
        else if (move = btn_Move2)
            currentMove = HPUNCH;
        else if (move = btn_Move3)
            currentMove = LKICK;
        else if (move = btn_Move4)
            currentMove = HKICK;
    }

    void DisableMoveButtons()
    {
        btn_Move1.interactable = false;
        btn_Move2.interactable = false;
        btn_Move3.interactable = false;
        btn_Move4.interactable = false;
    }
    
    void EnableMoveButtons()
    {
        btn_Move1.interactable = true;
        btn_Move2.interactable = true;
        btn_Move3.interactable = true;
        btn_Move4.interactable = true;
    }

    public void EnterFightMode()
    {
        currentState = BattleState.FIGHT;
        EnableMoveButtons();
    }

    public void EnterItemsMode()
    {
        DisableMoveButtons();
        currentState = BattleState.ITEMS;
    }

    public void EnterWeaponsMode()
    {
        DisableMoveButtons();
        currentState = BattleState.WEAPONS;
    }

    public void EnterRunMode()
    {
        DisableMoveButtons();
        currentState = BattleState.RUN;
    }
}
