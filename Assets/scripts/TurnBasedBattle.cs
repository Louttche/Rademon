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
        RUN,
    }

    private BattleState currentState;
    private Image PlayerHealthBar;
    private Image EnemyHealthBar;
    public Image img_PlayerRadiationStatus;
    //public Image img_EnemyRadiationStatus; Not needed for demo
    private Moves currentMove;
    private Button btn_Move1;
    private Button btn_Move2;
    private Button btn_Move3;
    private Button btn_Move4;
    private Button btn_Fight;
    private Button btn_Items;
    private Button btn_Weapons;
    private Button btn_Run;
    private bool PlayerTurn = true;
    private Stats PlayerStats;
    private Stats EnemyStats;
    private Moves LPUNCH; //light punch
    private Moves HPUNCH; //heavy punch
    private Moves LKICK; //light kick
    private Moves HKICK; //heavy kick
    private int TutorialEnemyTurn;
    public DialogueManager dialogueManager;
    public Dialogue BattleStartDialogue;
    public Dialogue FirstAttackDialogue;
    public Dialogue RadiationDialogue;
    public Dialogue WinningDialogue;
    public Dialogue LosingDialogue;
    private bool EndofBattle;

    // Use this for initialization
    void Start () {
        InitializeStats();
        InitializeMoves();
        InitializeMoveButtons();
        InitializeChoiceButtons();
        DisableMoveButtons();
        DisableChoicesButtons();
        currentState = BattleState.START;
        TutorialEnemyTurn = 0;
        img_PlayerRadiationStatus.enabled = false;
        EndofBattle = false;
        //dialogueManager.EndofDialogue = true; //Not needed as BattleStartDialogue will set it as true when it's done
        dialogueManager.StartDialogue(BattleStartDialogue);
    }

    private void Update()
    {
        if (EndofBattle == false)
        {
            if ((PlayerStats.IsDead == false) && (EnemyStats.IsDead == false) && (dialogueManager.EndofDialogue == true)) //Checks whether the player or enemy is dead and go to the player's turn only if all dialogues are done
                TurnChecker();
            else if (PlayerStats.IsDead == true)
            {
                Debug.Log("Player died");
                dialogueManager.StartDialogue(LosingDialogue);
                EndofBattle = true;
            }
            else if (EnemyStats.IsDead == true)
            {
                Debug.Log("Enemy died");
                dialogueManager.StartDialogue(WinningDialogue);
                EndofBattle = true;
            }
        }
        else
        {
            if (dialogueManager.EndofDialogue == true)
            {
                Debug.Log("Battle ended.");
                Application.Quit();
            }
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax) //Returns the damage value according to fillamount's range (0 - 1)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    void InitializeMoveButtons()
    {
        btn_Move1 = this.transform.Find("Canvas/MainPanel/MovesPanel/Move1").gameObject.GetComponent<Button>();
        btn_Move2 = this.transform.Find("Canvas/MainPanel/MovesPanel/Move2").gameObject.GetComponent<Button>();
        btn_Move3 = this.transform.Find("Canvas/MainPanel/MovesPanel/Move3").gameObject.GetComponent<Button>();
        btn_Move4 = this.transform.Find("Canvas/MainPanel/MovesPanel/Move4").gameObject.GetComponent<Button>();
    }

    void InitializeChoiceButtons()
    {
        btn_Fight = this.transform.Find("Canvas/MainPanel/ChoicesPanel/Fight").gameObject.GetComponent<Button>();
        btn_Items = this.transform.Find("Canvas/MainPanel/ChoicesPanel/Items").gameObject.GetComponent<Button>();
        btn_Weapons = this.transform.Find("Canvas/MainPanel/ChoicesPanel/Weapons").gameObject.GetComponent<Button>();
        btn_Run = this.transform.Find("Canvas/MainPanel/ChoicesPanel/Run").gameObject.GetComponent<Button>();
    }

    void DisableChoicesButtons()
    {
        btn_Fight.interactable = false;
        btn_Items.interactable = false;
        btn_Weapons.interactable = false;
        btn_Run.interactable = false;
    }

    void EnableChoicesButtons()
    {
        btn_Fight.interactable = true;
        btn_Items.interactable = true;
        btn_Weapons.interactable = true;
        btn_Run.interactable = true;
    }

    void InitializeStats()
    {
        PlayerStats = Stats.CreateComponent(100, false);
        EnemyStats = Stats.CreateComponent(50, false);
        PlayerHealthBar = this.transform.Find("Canvas/BattleScene/Player/StatsLayout/HealthBar Background/HealthBar").gameObject.GetComponent<Image>();      
        EnemyHealthBar = this.transform.Find("Canvas/BattleScene/Foe/StatsLayout/HealthBar Background/HealthBar").gameObject.GetComponent<Image>();
        PlayerHealthBar.fillAmount = 1;
        EnemyHealthBar.fillAmount = 1;
    }

    void InitializeMoves()
    {
        LPUNCH = Moves.CreateComponent("Light Punch", 10, 5);
        HPUNCH = Moves.CreateComponent("Heavy Punch", 20, 2);
        LKICK = Moves.CreateComponent("Light Kick", 15, 3);
        HKICK = Moves.CreateComponent("Heavy Kick", 25, 1);
    }

    private void TurnChecker()
    {
        Debug.LogFormat("Player's health is {0}", PlayerStats.CurrentHealth);
        Debug.LogFormat("Enemy's health is {0}", EnemyStats.CurrentHealth);

        if (PlayerTurn == true)
        {
            Debug.Log("Player's turn");
            EnableChoicesButtons();            
            switch (currentState)
            {
                case BattleState.START:
                    break;

                case BattleState.FIGHT:
                    EnemyStats.IsDead = EnemyStats.DecreaseHealth(currentMove.Damage);
                    if (EnemyStats.IsDead == false) //Set health bar to 0 if dead
                        EnemyHealthBar.fillAmount -= Map(currentMove.Damage, 0, 50, 0, 1);
                    else
                        EnemyHealthBar.fillAmount = 0;
                    bool TookRadiationDmg = TakeRadiationDamage(PlayerStats);
                    if (TookRadiationDmg == true)
                    {                       
                        PlayerHealthBar.fillAmount -= 0.05f;
                        Debug.LogFormat("You took radiation damage, your health is now {0}", PlayerStats.CurrentHealth);
                    }
                    DisableMoveButtons();
                    currentMove = null;
                    PlayerTurn = false;
                    currentState = BattleState.START;
                    break;

                case BattleState.ITEMS:
                    //Open Item inventory
                    DisableMoveButtons();
                    Debug.Log("Items not yet implemented");
                    currentState = BattleState.START;
                    break;

                case BattleState.RUN:
                    Debug.Log("Escaped Rademon");
                    SceneManager.LoadScene("MainScene");
                    break;

                case BattleState.WEAPONS:
                    //Check Available Weapons
                    DisableMoveButtons();
                    Debug.Log("Weapons not yet implemented");
                    currentState = BattleState.START;
                    break;
            }
        }
        else //Enemy's turn
        {            
            Debug.Log("Enemy's turn");
            DisableChoicesButtons();
            TutorialEnemyTurn++;
            if (TutorialEnemyTurn == 1) //On the first turn just deal some damage to the player and have Vaultboi explain what happened
            {
                PlayerStats.IsDead = PlayerStats.DecreaseHealth(20);
                PlayerHealthBar.fillAmount -= 0.2f; //No need to check if player is dead in the first turn
                Debug.Log("Enemy attacked you!");
                dialogueManager.StartDialogue(FirstAttackDialogue);
            }           
            else if (TutorialEnemyTurn == 2) //On the second turn have the player take radiation
            {
                PlayerStats.IsDead = PlayerStats.DecreaseHealth(5);
                PlayerHealthBar.fillAmount = PlayerHealthBar.fillAmount - 0.05f;
                PlayerStats.IsRadiated = true;
                img_PlayerRadiationStatus.enabled = true;
                Debug.Log("You became radiated!");
                dialogueManager.StartDialogue(RadiationDialogue);
            }
            else //next turns just keep damaging the player lightly
            {
                PlayerStats.IsDead = PlayerStats.DecreaseHealth(10);
                if (PlayerStats.IsDead == false) //Set health bar to 0 if dead
                    PlayerHealthBar.fillAmount -= 0.1f;
                else
                    PlayerHealthBar.fillAmount = 0;
                Debug.Log("Enemy attacked you!");
            }
            PlayerTurn = true;
        }
    }

    bool TakeRadiationDamage(Stats who) //return true if taken damage by radiation
    {
        if (who.IsRadiated == true)
        {
            who.DecreaseHealth(5);
            return true;
        }
        return false;
    }

    public void CheckMoveClicked(Button btn)
    {                      
        if (btn == btn_Move1)
            currentMove = LPUNCH;
        else if (btn == btn_Move2)
            currentMove = HPUNCH;
        else if (btn == btn_Move3)
            currentMove = LKICK;
        else if (btn == btn_Move4)
            currentMove = HKICK;        
        bool EnoughPP = currentMove.DecreasePP();
        if (EnoughPP == true)
        {
            Debug.LogFormat("Used Move: {0} with damage of {1} and PP of {2}", currentMove.Name, currentMove.Damage, currentMove.CurrentPP);
            currentState = BattleState.FIGHT;
        }           
    }

    void DisableMoveButtons()
    {
        btn_Move1.interactable = false;
        btn_Move2.interactable = false;
        btn_Move3.interactable = false;
        btn_Move4.interactable = false;
    }
    
    public void EnableMoveButtons()
    {
        btn_Move1.interactable = true;
        btn_Move2.interactable = true;
        btn_Move3.interactable = true;
        btn_Move4.interactable = true;
    }

    public void EnterItemsMode()
    {
        Debug.Log("Entered Items Mode.");
        currentState = BattleState.ITEMS;
    }

    public void EnterWeaponsMode()
    {
        Debug.Log("Entered Weapons Mode.");
        currentState = BattleState.WEAPONS;
    }

    public void EnterRunMode()
    {
        Debug.Log("Entered Run Mode.");
        currentState = BattleState.RUN;
    }
}
