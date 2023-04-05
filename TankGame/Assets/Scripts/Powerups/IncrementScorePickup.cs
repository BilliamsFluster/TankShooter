using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementScorePickup : Pickup
{
    public IncrementScorePowerup powerup;
    void Start()
    {

    }

    // Update is called once per frame
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
