using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketLauncherScript : MonoBehaviour
{
    Mouse m;
    [Header("Primary Fire")]
    public Camera playerCamera;
    public Transform RocketStart;
    public GameObject rocketPrefab;
    float firerate = 2f;
    private float fireTime = 0f;
    public LayerMask ignoredMasks;
    void Start()
    {
        m = InputSystem.GetDevice<Mouse>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(m.leftButton.wasPressedThisFrame && (Time.time >= fireTime)){
            Shoot();
        }
    }

    private void Shoot(){
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit target;
        Vector3 center;

        //~playerProjectile - avoid casting to other bullets
        if(Physics.Raycast(ray, out target, 1000f, ~ignoredMasks))
            center = target.point;
        else
            center = ray.GetPoint(500);

        fireTime = Time.time + firerate;
        GameObject rocket = Instantiate(rocketPrefab, RocketStart.position, RocketStart.rotation);
        rocket.GetComponent<Rigidbody>().velocity = (center - RocketStart.transform.position).normalized*15;
    }
}
