using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform playerCamera;
    public int damage = 37;

    private Vector3 previousPos;
    private Vector3 currentPos;
    private Vector3 velocityDirection;
    private float stepSize;
    private Rigidbody rb;

    private RaycastHit hit;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10);

        previousPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Cast a ray from the new position to the old and check collisions -- build in physics not fast enough
        velocityDirection = rb.velocity.normalized;
        currentPos = transform.position;

        stepSize = (currentPos - previousPos).magnitude;
        if (stepSize > 0.01)
        {
            if (Physics.Raycast(previousPos, velocityDirection, out hit, stepSize, layerMask))
            {
                ITakeDamage enemy = hit.collider.transform.GetComponent<ITakeDamage>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage, "rifle");
                    Destroy(gameObject);
                }
                Destroy(gameObject);
            }
            else
            {
                previousPos = currentPos;
            }
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            ITakeDamage hit = other.gameObject.GetComponent<ITakeDamage>();
            if (hit != null)
            {
                hit.takeDamage(damage, "rifle");
                print(other.gameObject.name);
                Destroy(gameObject);
            }
            
        }

        Destroy(gameObject);
    }*/
    
}
