using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RifleScript : MonoBehaviour
{
    Mouse m;
    [Header("Primary Fire Settings")]
    public Camera playerCamera;
    public Transform BulletStart;
    public GameObject bulletPrefab;
    float firerate = 0.25f;
    private float fireTime = 0f;
    public LayerMask playerProjectile;

    [Header("Alt Fire Settings")]
    int altFireDamage = 10000;
    float altFireRange = 500;
    
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        m = InputSystem.GetDevice<Mouse>();
        line.useWorldSpace = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m.rightButton.isPressed && m.leftButton.wasPressedThisFrame){
            StartCoroutine(AltShoot());
        } else if(m.leftButton.wasPressedThisFrame && (Time.time >= fireTime) && !m.rightButton.isPressed || m.leftButton.isPressed && (Time.time >= fireTime) && !m.rightButton.isPressed){
            Shoot();
        }
        
    }

    IEnumerator AltShoot()
    {
        Vector3 center;
        RaycastHit hit;
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward, out hit, altFireRange)){
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null){
                enemy.takeDamage(altFireDamage);
            }
            line.SetPosition(0, BulletStart.position);
            line.SetPosition(1, hit.point);
        } else{
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            center = ray.GetPoint(500);
            line.SetPosition(0, BulletStart.position);
            line.SetPosition(1, center);
        }

        line.enabled = true;
        yield return new WaitForSeconds(0.02f);
        line.enabled = false;

    }

    private void Shoot(){
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit target;
        Vector3 center;

        //~playerProjectile - avoid casting to other bullets
        if(Physics.Raycast(ray, out target, ~playerProjectile))
            center = target.point;
        else
            center = ray.GetPoint(500);

        fireTime = Time.time + firerate;
        GameObject bullet = Instantiate(bulletPrefab, BulletStart.position, BulletStart.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (center - BulletStart.transform.position).normalized*25;
    }
}
