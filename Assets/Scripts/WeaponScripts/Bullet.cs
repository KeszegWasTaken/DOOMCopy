using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform playerCamera;
    public int damage = 37;
    
    // Start is called before the first frame update
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
