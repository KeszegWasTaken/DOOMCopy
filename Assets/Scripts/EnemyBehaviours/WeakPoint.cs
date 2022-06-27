using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, ITakeDamage
{
    public string weakPointType;
    [SerializeField] protected int health;
    private ITakeDamage parentDamage;
    public QuestContoller qc;
    public int Health
    {
        get { return health;}
        set { health = value; }
    }
    
    void Start(){
        qc = GameObject.FindWithTag("QC").GetComponent<QuestContoller>();
        parentDamage = transform.root.GetComponent<ITakeDamage>();
        Health = 1000;
    }

    public void WeakPointDestroyed() {}

    public void takeDamage(int damage, string source)
    {   
        Health -= damage;
        parentDamage?.takeDamage(damage, source);
        if (Health < 1)
        {
            parentDamage.WeakPointDestroyed();
            gameObject.SetActive(false);
            qc.QuestLookUp(source, transform.root.name);
        }
        
    }

    
}
