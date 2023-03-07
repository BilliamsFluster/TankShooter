using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;

    public override void Apply(PowerupManager target)
    {
        HealthManager targetHealth = target.GetComponent<HealthManager>();

        if(targetHealth != null)
        {
            targetHealth.Heal(healthToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {

    }

}
