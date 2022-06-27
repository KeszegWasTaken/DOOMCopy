using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBall : MonoBehaviour
{
    Transform playerCamera;
    public int damage = 25;
    
    private Vector3 previousPos;
    private Vector3 currentPos;
    private Vector3 velocityDirection;
    private float stepSize;
    private Rigidbody rb;

    private RaycastHit hit;
    public LayerMask layerMask;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10);

        previousPos = transform.position;
    }

    void FixedUpdate()
    {
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
                    enemy.takeDamage(damage, "plasma");
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

    /*void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            ITakeDamage hit = other.collider.gameObject.GetComponent<ITakeDamage>();
            if (hit != null)
            {
                hit.takeDamage(damage, "plasma");
            }
        }
        Destroy(gameObject);
    }*/
}