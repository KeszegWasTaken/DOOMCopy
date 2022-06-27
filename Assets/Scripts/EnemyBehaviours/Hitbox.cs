using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, ITakeDamage
{
    private ITakeDamage root;
    void Start()
    {
        root = transform.root.GetComponent<ITakeDamage>();
    }
    

    public int Health { get; set; }
    public void WeakPointDestroyed() {}

    public void takeDamage(int damage, string source)
    {
        if (source.Equals("rocket"))
        {
            root.takeDamage(damage/5, source);
        }
        else
        {
            root.takeDamage(damage, source);
        }
    }
}
