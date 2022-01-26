using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlasmaRifleScript : MonoBehaviour
{
    Mouse m;
    [Header("Primary Fire Settings")]
    public Camera playerCamera;
    public Transform PlasmaStart;
    public GameObject plasmaPrefab;
    float firerate = 0.1f;
    private float fireTime = 0f;

    [Header("Alt Fire Settings")]
    int altFireDamage = 750;
    
    public LayerMask ignoredMasks;

    public int charge = 0;
    int chargeLimit = 50;
    void Start()
    {
        m = InputSystem.GetDevice<Mouse>();
        playerCamera = Camera.main;
    }

    
    void Update()
    {
        if(m.rightButton.wasPressedThisFrame && (charge>=chargeLimit)){
            AltShoot();
        } else if((m.leftButton.wasPressedThisFrame && (Time.time >= fireTime)) || (m.leftButton.isPressed && (Time.time >= fireTime)) ){
            Shoot();
        }
        
    }

    private void AltShoot()
    {
        
        Collider[] colliders = Physics.OverlapBox(PlasmaStart.transform.position + (PlasmaStart.transform.forward*3), PlasmaStart.transform.localScale*2, Quaternion.identity, ~ignoredMasks);

        foreach(Collider inRange in colliders){
            if(inRange.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                Enemy enemy = inRange.gameObject.GetComponent<Enemy>();
                if(enemy != null){
                    enemy.takeDamage(altFireDamage);
                } 
            } else if(inRange.gameObject.layer == LayerMask.NameToLayer("EnemyWeakpoint")){
                WeakPoint wp = inRange.gameObject.GetComponent<WeakPoint>();
                if(wp != null){
                    wp.wpTakeDamage(altFireDamage);
                }
            }
        }
        charge = 0;
    }

    private void Shoot(){
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit target;
        Vector3 center;

        //~playerProjectile - avoid casting to other bullets
        if(Physics.Raycast(ray, out target, 1000f, ~ignoredMasks)){
            center = target.point;}
        else
            center = ray.GetPoint(500);

        fireTime = Time.time + firerate;
        charge++;
        GameObject bullet = Instantiate(plasmaPrefab, PlasmaStart.position, PlasmaStart.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (center - PlasmaStart.transform.position).normalized*15;
    }
}

