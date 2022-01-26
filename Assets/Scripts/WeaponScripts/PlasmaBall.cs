using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBall : MonoBehaviour
{
    Transform playerCamera;
    public int damage = 25;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
        playerCamera = Camera.main.transform;
    }
    void Update()
    {
        if((gameObject.transform.position - playerCamera.position).magnitude > 500 ){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision){
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.takeDamage(damage);
            } 
        } else if(collision.collider.gameObject.layer == LayerMask.NameToLayer("EnemyWeakpoint")){
                WeakPoint wp = collision.collider.gameObject.GetComponent<WeakPoint>();
                if(wp != null){
                    wp.wpTakeDamage(damage);
                }
            }
        Destroy(gameObject);
    }
}
