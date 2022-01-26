using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public int health = 500;

    public Enemy parent;
    // Start is called before the first frame update
    
    void Start(){
        parent = gameObject.GetComponentInParent<Enemy>();
    }
    public void wpTakeDamage(int damage){
        health -= damage;
        parent.takeDamage(damage);

        if(health <= 0){
            DestroyWeakPoint();
        }
    }

    private void DestroyWeakPoint()
    {
        Destroy(gameObject);
    }
}
