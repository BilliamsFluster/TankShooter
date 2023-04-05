using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    [HideInInspector]
    public Pawn pawn;
   
    public int TankScore = 0;

    public void AddScore(int score)
    {
        TankScore += score;
    }

   
    protected virtual void Start()
    {
        
        

    }
    protected virtual void Awake()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    
   
}
