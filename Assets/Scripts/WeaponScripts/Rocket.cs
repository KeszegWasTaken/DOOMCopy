using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Transform playerCamera;
    public int damage = 1000;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
        playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if((gameObject.transform.position - playerCamera.position).magnitude > 500 ){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision){
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        foreach(Collider inRange in colliders){
            if(inRange.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                Enemy enemy = inRange.gameObject.GetComponent<Enemy>();
                if(enemy != null){
                    enemy.takeDamage(damage);
                } 
            } else if(inRange.gameObject.layer == LayerMask.NameToLayer("EnemyWeakpoint")){
                WeakPoint wp = inRange.gameObject.GetComponent<WeakPoint>();
                if(wp != null){
                    wp.wpTakeDamage(damage);
                }
            }
        }
        Destroy(gameObject);
    }
}
