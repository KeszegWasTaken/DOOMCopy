using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestContoller : MonoBehaviour
{
    public List<Quest> active;
    public List<Quest> completed;

    private void Start()
    {
        active = new List<Quest>();
        active.Add(new Quest(1, false, "rifle", "Rocketeer"));
        completed = new List<Quest>();
    }

    public void QuestLookUp(string source, string target)
    {
        for (int i = 0; i < active.Count; i++)
        {
            Quest q = active[i];
            if (q.Source.Equals(source) && q.Target.Equals(target))
            {
                q.Progress++;

                if (q.Progress == q.Total)
                {
                    q.IsComplete = true;
                    completed.Add(q);
                    active.Remove(q);
                    //print(completed[0].Target + "killed by " +completed[0].Source+", "+completed[0].Progress+"/"+completed[0].Total);
                }
            }
        }
    }
    
    
    public void LoadQuests()
    {
        //TODO
    }
}
