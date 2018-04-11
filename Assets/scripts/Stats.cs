using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public bool IsRadiated { get; set; }
    public bool IsDead { get; set; }

    //public AudioSource Hit_sound;
    //public AudioSource Radiation_sound;

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

    public static Stats CreateComponent(float maxhealth, bool isRadiated)
    {
        var NewStat = BattleController.AddComponent<Stats>();
        NewStat.MaxHealth = maxhealth;
        NewStat.CurrentHealth = maxhealth;
        NewStat.IsRadiated = isRadiated;
        NewStat.IsDead = false;
        return NewStat;
    }

    public void IncreaseHealth(float amount)
    {
        this.CurrentHealth += amount;
        if (this.CurrentHealth > this.MaxHealth)
            this.CurrentHealth = this.MaxHealth;
    }

    public bool DecreaseHealth(float amount) //returns true if it died from the damage
    {
        //Hit_sound = this.transform.Find("Hit").GetComponent<AudioSource>();
        //Hit_sound.Play();
        this.CurrentHealth -= amount;
        if (CurrentHealth <= 0)
            return true;
        return false;
    }

    public void CureRadiation()
    {
        this.IsRadiated = false;
    }

    public void GetRadiation()
    {
        //Radiation_sound = this.transform.Find("Radiation").GetComponent<AudioSource>();
        //Radiation_sound.Play();
        this.IsRadiated = true;
    }
}
