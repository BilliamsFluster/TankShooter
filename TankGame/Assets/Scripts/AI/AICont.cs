using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICont : Controller
{
   
    public enum AIState { Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost, Idle};
    public AIState currentState;
    private float lastStateChangeTime;

    

    //void Start()
    //{
    //    ChangeState(AIState.Idle);
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    protected void DoIdleState()
    {

    }
    
    public void Seek(Vector3 targetPosition)
    {

    }
    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChangeTime = Time.time;
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if(Vector3.Distance(gameObject.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
