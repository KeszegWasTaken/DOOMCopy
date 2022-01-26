using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10000;
    // Start is called before the first frame update
    public void takeDamage(int damage){
        health -= damage;

        if(health <= 0){
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
        //TODO add death effect
    }
}
