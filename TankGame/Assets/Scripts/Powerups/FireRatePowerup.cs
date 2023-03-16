using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FireRatePowerup : Powerup
{
    // Start is called before the first frame update
    public float FireRateDecrease = 0.5f;


    public override void Apply(PowerupManager target)
    {
        Gun tankGun = target.GetComponentInChildren<Gun>();

        if (tankGun != null && target.tag == "Player")
        {
            tankGun.coolDownTime -= FireRateDecrease;
        }

        if (tankGun != null && target.tag == "Enemy")
        {
            AIController AI = target.GetComponent<AIController>();
            if (AI)
            {
                AI.timeBetweenAttacks -= FireRateDecrease;
            }
        }
    }

    public override void Remove(PowerupManager target)
    {
        Gun tankGun = target.GetComponentInChildren<Gun>();

        if (tankGun != null && target.tag == "Player")
        {
            tankGun.coolDownTime += FireRateDecrease;
        }

        if (tankGun != null && target.tag == "Enemy")
        {
            AIController AI = target.GetComponent<AIController>();
            if (AI)
            {
                AI.timeBetweenAttacks += FireRateDecrease;
            }
        }
    }

}
