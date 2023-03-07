using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup 
{
    public float duration;
    
    //[HideInInspector]
    public float currentDuration;
    public bool canStack;
    
    
    public int maxStack;
    public int stack;

    public bool isPermanent;
    
    protected void Start()
    {
        currentDuration = duration;
    }

    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);


}
