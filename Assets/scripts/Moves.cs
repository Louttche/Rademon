using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour {

    public string Name { get; set; }
    public float Damage { get; set; }
    public int MaxPP { get; set; }
    public int CurrentPP;

    private static GameObject battleController;
    public static GameObject BattleController
    {
        get
        {
            if (battleController == null)
            {
                battleController = new GameObject("Battle Controller");
            }
            return battleController;
        }
    }

    public static Moves CreateComponent(string name, float damage, int pp)
    {
        var NewMove = BattleController.AddComponent<Moves>();
        NewMove.Name = name;
        NewMove.Damage = damage;
        NewMove.MaxPP = pp;
        NewMove.CurrentPP = NewMove.MaxPP;
        return NewMove;
    }

    public bool DecreasePP() //Decreases PP if needed, but if there is no more PP left then returns false
    {
        if (this.CurrentPP == 0)
        {
            Debug.Log("Can't use move, no more PP left.");
        }
        else if (this.CurrentPP > 0)
        {
            this.CurrentPP--;
            return true;
        }
        return false;
    }

    public bool IncreasePP(int amount) //Increases PP if item is used, but if there it's already at max PP then returns false
    {
        if (this.CurrentPP == MaxPP)
        {
            Debug.Log("PP for this move already at max");
        }
        else if (this.CurrentPP < MaxPP)
        {
            this.CurrentPP += amount;
            return true;
        }
        return false;
    }
}
