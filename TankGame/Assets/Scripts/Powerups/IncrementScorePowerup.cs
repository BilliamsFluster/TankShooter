using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IncrementScorePowerup : Powerup
{
    public int scoreToAdd;

    public override void Apply(PowerupManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>(); 

        if (tankPawn != null)
        {
            tankPawn.tankController.AddScore(scoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {

    }

}
