using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    public int health;
    public int shield;
    public int Health
    {
        get { return health;}
        set { health = value; }
    }
    void Start()
    {
        health = 100;
        shield = 50;
    }
    public void WeakPointDestroyed() {}

    public void takeDamage(int damage, string source)
    {
        if (damage > shield)
        {
            shield = 0;
            Health -= damage;

            if (Health < 1)
            {
               Debug.Log("Player died.");
               Health = 0;
            }
        }
        else
        {
            shield -= damage;
        }
    }
}
