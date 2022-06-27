using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketeerProjectile : MonoBehaviour
{
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach(Collider inRange in colliders){
            if(inRange.gameObject.layer == LayerMask.NameToLayer("Player")){
                ITakeDamage hit = inRange.gameObject.GetComponent<ITakeDamage>();
                if (hit != null)
                {
                    hit.takeDamage(damage, "");
                }
            }
        }
        Destroy(gameObject);
    }
}
