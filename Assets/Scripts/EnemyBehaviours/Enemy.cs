using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public abstract class Enemy : MonoBehaviour, ITakeDamage
{
    protected NavMeshAgent navMeshAgent;
    protected Transform self;
    public int health;
    public int Health
    {
        get { return health;}
        set { health = value;  }
    }
    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float rangeNoWeakpoint;
    [SerializeField]
    protected int weakPointCount;

    public Transform player;
    public Animator animator;
    public QuestContoller qc;

    private float distanceToPlayer;
    private float distanceToPlayerOnMesh;
    public LayerMask layerMask;
    private bool isDead;

    private void Start()
    {
        Init();
        Stats();
    }

    protected virtual void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        self = gameObject.transform;
        player = GameObject.FindWithTag("Player").transform;
        isDead = false;
        qc = GameObject.FindWithTag("QC").GetComponent<QuestContoller>();

        weakPointCount = CountWeakpoints();
        //TODO Animator stuff
        animator = GetComponent<Animator>();
    }

    protected virtual void Stats()
    {
        movementSpeed = 5f;
        range = 5f;
        rangeNoWeakpoint = 3f;

        Health = 10000;
    }

    protected virtual void Update()
    {
        //Distance of enemy to the player
        distanceToPlayer = (player.position - self.position).magnitude;
        distanceToPlayerOnMesh = navMeshAgent.remainingDistance;
        if (!isDead){
            {
                if (distanceToPlayer < 30)
                {
                    navMeshAgent.SetDestination(player.position);
                    navMeshAgent.isStopped = false;
                    Move();


                }
                else
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("walking", false);
                }
            }
        }

    }

    protected virtual void Move()
    {
        
        if (weakPointCount > 0 && 0.5 < distanceToPlayerOnMesh && distanceToPlayerOnMesh < range)
        {
            if (LineOfSight(range))
            {
                navMeshAgent.isStopped = true;
                LookAtPlayer();
                Attack(true);
                animator.SetBool("walking", false);
                
            }
            else
            {
                navMeshAgent.isStopped = false;
                //MoveToPlayer();
                animator.SetBool("walking", true);
            }
            
        }
        else
        {
            if (weakPointCount == 0 && 0.2 < distanceToPlayerOnMesh &&  distanceToPlayerOnMesh < rangeNoWeakpoint)
            {
                navMeshAgent.isStopped = true;
                animator.SetBool("walking", false);
                if (animator.GetBool("isAttacking") == false)
                {
                    LookAtPlayer();
                    Attack(false);
                }
                
            }
            else
            {
                navMeshAgent.isStopped = false;
                //MoveToPlayer();
                animator.SetBool("walking", true);
                
            }
        }
    }

    protected virtual void Attack(bool wps)
    {
        //Implemented in inherited class
        //If called with true, attack from close, otherwise from afar
    }
    
    private int CountWeakpoints()
    {
        /*int count = 0;
        //Counts only DIRECT children
        foreach (Transform weakpoints in transform)
        {
            WeakPoint wp = weakpoints.GetComponent<WeakPoint>();
            if (wp !=null)
            {
                count++;
            }
        }*/

        return GetComponentsInChildren<WeakPoint>().Length;
    }

    private void LookAtPlayer()
    {
        Vector3 position = self.position;
        
        Vector3 lookAt = new Vector3(player.position.x - position.x, 0f, player.position.z - position.z).normalized;
        self.forward = lookAt;
    }

    /*private void MoveToPlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }*/

    private bool LineOfSight(float attackRange)
    {
        RaycastHit hit;
        Vector3 rayOrigin = self.position + new Vector3(0, 1, 0);
        Debug.DrawRay(rayOrigin, (player.position-self.position).normalized*attackRange, Color.red);
        if (Physics.Raycast(rayOrigin, (player.position-self.position).normalized, out hit, attackRange, layerMask))
        {
            if ( 1 << hit.transform.gameObject.layer == 1 << 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void takeDamage(int damage, string source)
    {
        Health -= damage;
        if (Health < 1)
        {
            animator.SetTrigger("death");
            isDead = true;
            qc.QuestLookUp(source, transform.root.name);
            navMeshAgent.isStopped = true;
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void WeakPointDestroyed()
    {
        weakPointCount--;
    }
}
