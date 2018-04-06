using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public Image HealthBar;

    private float CurrentHealth;
    public float MaxHealth { get; set; }
    public bool IsRadiated { get; set; }
    public bool IsDead;
    
    public Stats(float maxhealth, bool isRadiated)
    {
        this.MaxHealth = maxhealth;
        this.CurrentHealth = maxhealth;
        this.IsRadiated = isRadiated;
        this.IsDead = false;
        HealthBar.fillAmount = 1;
    }

    public void IncreaseHealth(float amount)
    {
        if (CurrentHealth + amount >= MaxHealth)
        {
            this.CurrentHealth = MaxHealth;
            HealthBar.fillAmount = 1; 
        }
        else
        {
            this.CurrentHealth += amount;
            HealthBar.fillAmount += amount/100;
        }
    }

    public void DecreaseHealth(float amount)
    {
        if (CurrentHealth - amount <= 0)
        {
            this.CurrentHealth = 0;
            HealthBar.fillAmount = 0;
            this.IsDead = true;
        }
        else
        {
            this.CurrentHealth -= amount;
            HealthBar.fillAmount -= amount / 100;
        }
    }

    public void CureRadiation()
    {
        this.IsRadiated = false;
    }

    public void GetRadiation()
    {
        this.IsRadiated = true;
    }
}
