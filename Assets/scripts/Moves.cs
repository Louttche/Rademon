using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour {

    public float Damage { get; set; }
    public int PP { get; set; }

    public Moves(float damage, int pp)
    {
        this.Damage = damage;
        this.PP = pp;
    }
}
