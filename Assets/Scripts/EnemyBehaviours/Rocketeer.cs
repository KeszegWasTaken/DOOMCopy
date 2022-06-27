using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Rocketeer : Enemy
{
    private float rangedAttackCooldown = 3f;
    private float meleeAttackCooldown  = 3f;
    private bool attacked = false;

    public GameObject launchpoint1;
    public GameObject launchpoint2;

    public GameObject rangedProjectile;
    protected override void Stats()
    {
        movementSpeed = 5f;
        range = 20f;
        rangeNoWeakpoint = 5f;
        Health = 3000;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack(bool wps)
    {
        if (wps)
        {
            if (!attacked)
            {
                if (launchpoint1.activeInHierarchy)
                {
                    Rigidbody rb = Instantiate(rangedProjectile, launchpoint1.transform.position, Quaternion.LookRotation(((player.position + new Vector3(0, 1.5f, 0))-launchpoint1.transform.position).normalized)).GetComponent<Rigidbody>();
                    rb.AddForce(rb.transform.forward*16f, ForceMode.Impulse);
                }

                if (launchpoint2.activeInHierarchy)
                {
                    Rigidbody rb2 = Instantiate(rangedProjectile, launchpoint2.transform.position, Quaternion.LookRotation(((player.position + new Vector3(0, 1.5f, 0))-launchpoint2.transform.position).normalized)).GetComponent<Rigidbody>();
                    rb2.AddForce(rb2.transform.forward*16f, ForceMode.Impulse);
                }
                
                
                attacked = true;
                Invoke(nameof(attackReset), rangedAttackCooldown);
            }
        }
        else
        {
            if (!attacked)
            {
                animator.SetTrigger("attack");
                animator.SetBool("isAttacking", true);
                attacked = true;
                Invoke(nameof(attackReset), meleeAttackCooldown);
            }
        }
    }

    private void attackReset()
    {
        attacked = false;
    }

    private void setBoolToFalse()
    {
        animator.SetBool("isAttacking", false);
    }
}
