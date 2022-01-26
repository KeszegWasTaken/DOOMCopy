using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RevenantBehaviour : MonoBehaviour
{

    NavMeshAgent navMeshAgent;

    public GameObject player;
    public float movementSpeed = 9f;
    public float attackRange = 25f;
    public float attackRangeNoWeakpoints = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayer();
        float distance = (player.transform.position - gameObject.transform.position).magnitude;
        if(gameObject.transform.childCount >= 1){

            if(distance >= attackRange && distance < 200){
            } else{
                //TODO attack the player
                print("Attack!");
            }
        } else{
            if(distance >= attackRangeNoWeakpoints && distance < 200){
            } else{
                //TODO attack the player
                print("Attack from close!");
            }
        }

    }

    private void TargetPlayer(){
        navMeshAgent.SetDestination(player.transform.position);
    }
}
