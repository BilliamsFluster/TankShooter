using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePickup : Pickup
{

    public FireRatePowerup powerup;
    
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        if (powerupManager != null)
        {
            powerupManager.Add(powerup);

            Destroy(gameObject);
        }
    }
}
