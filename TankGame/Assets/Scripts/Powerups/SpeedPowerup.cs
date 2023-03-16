using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable]
public class SpeedPowerup : Powerup
{

    public float speedToAdd;
    public float playerTankSpeedMultiplier = 4f;

    public override void Apply(PowerupManager target)
    {
        TankMovement tankMovement = target.GetComponent<TankMovement>();

        if (tankMovement != null && target.tag == "Player")
        {
            tankMovement.moveSpeed += speedToAdd * playerTankSpeedMultiplier;
        }

        if (target.tag == "Enemy")
        {
            NavMeshAgent AIAgent = target.GetComponent<NavMeshAgent>();
            if (AIAgent)
            {
                AIAgent.speed += speedToAdd;
            }
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankMovement tankMovement = target.GetComponent<TankMovement>();

        if (tankMovement != null && target.tag == "Player")
        {
            tankMovement.moveSpeed -= speedToAdd * playerTankSpeedMultiplier;
        }

        if (target.tag == "Enemy")
        {
            NavMeshAgent AIAgent = target.GetComponent<NavMeshAgent>();
            if (AIAgent)
            {
                AIAgent.speed -= speedToAdd;
            }
        }
    }


}
