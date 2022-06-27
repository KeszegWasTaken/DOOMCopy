using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        ITakeDamage hit = other.GetComponent<ITakeDamage>();
        if (hit != null)
        {
            hit.takeDamage(damage, "");
        }
    }
}
