using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunScript : MonoBehaviour
{
    public float range = 10f;
    public int damage = 1000;
    private float fireTime = 0f;
    public float fireRate = 2;
    Mouse m;


    public Camera playerCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        m = InputSystem.GetDevice<Mouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m.leftButton.wasPressedThisFrame && (Time.time >= fireTime)){
            fireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward, out hit, range)){
            ITakeDamage enemy = hit.collider.transform.GetComponent<ITakeDamage>();
            if (enemy != null)
            {
                enemy.takeDamage(damage, "shotgun");
            }
        }
    }
}
