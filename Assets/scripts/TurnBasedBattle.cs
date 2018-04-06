using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnBasedBattle : MonoBehaviour {

    enum BattleState
    {
        IDLE,
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
    public Button[] ChoicesButtons;

    private void Awake()
    {
        InitializeMoves();
        DisableMoveButtons();
        DisableChoicesButtons();
    }

    // Use this for initialization
    void Start () {
        PlayerStats = new Stats(100, false);
        EnemyStats = new Stats(50, false);
        currentState = BattleState.IDLE;
        PlayerTurn = true;
    }

    private void Update()
    {
        if ((PlayerStats.IsDead == false) && (EnemyStats.IsDead == false) && (currentState != BattleState.RUN))
            TurnChecker();
    }

    void DisableChoicesButtons()
    {
        foreach (Button b in ChoicesButtons)
        {
            b.interactable = false;
        }

    }

    void EnableChoicesButtons()
    {
        foreach (Button b in ChoicesButtons)
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
            EnableChoicesButtons();
            if (currentState == BattleState.ITEMS)
            {
                //Open Item inventory
                Debug.Log("Items not yet implemented");
            }
            else if (currentState == BattleState.WEAPONS)
            {
                //Check Available Weapons
                Debug.Log("Weapons not yet implemented");
            }
            else if (currentState == BattleState.FIGHT)                     //WORK ON THIS NEXT
            {
                //Change Enemy Stats
                if (currentMove != null)
                {
                    EnemyStats.DecreaseHealth(currentMove.Damage);                   
                    PlayerTurn = false;
                    currentMove = null;
                }
            }
            else if (currentState == BattleState.RUN)
            {
                //Message + Audio
                SceneManager.LoadScene("MainScene");
            }
        }
        else
        {
            //Hardcoded Enemy Attack
            PlayerStats.DecreaseHealth(10);
            PlayerTurn = true;
        }

    }

    public void CheckMoveClicked(Button btn)
    {
        if (btn = btn_Move1)
            currentMove = LPUNCH;
        else if (btn = btn_Move2)
            currentMove = HPUNCH;
        else if (btn = btn_Move3)
            currentMove = LKICK;
        else if (btn = btn_Move4)
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
