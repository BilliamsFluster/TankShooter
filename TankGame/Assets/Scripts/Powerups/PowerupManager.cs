using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    public List<Powerup> powerups;
    public List<Powerup> removedPowerupsQue;




    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupsQue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimer();
    }

    private void LateUpdate()
    {
        ApplyRemovePowerupsQue();
    }


    public void Add(Powerup powerupToAdd)
    {
        if (powerupToAdd.canStack == true && powerupToAdd.stack < powerupToAdd.maxStack) // for stacking option
        {
            powerupToAdd.Apply(this);
            powerups.Add(powerupToAdd);
            powerupToAdd.stack += 1;
        }
        else
        {
            if(powerupToAdd.canStack == false)
            {
                if(powerups.Count > 0)
                {
                    for (int i = 0; i < powerups.Count; i++)
                    {
                        if (powerupToAdd.GetType() == powerups[i].GetType()) // check fofr duplicate powerups
                        {
                            powerups[i].currentDuration = powerupToAdd.duration; // if we have a duplicate powerup just extend the time it lasts for to the inital set time. This prevents stacking of buffs

                        }
                        else
                        {
                            powerupToAdd.Apply(this);
                            powerups.Add(powerupToAdd);

                        }
                    }
                }
                if (powerups.Count <= 0) // first powerup
                {
                    powerupToAdd.Apply(this);
                    powerups.Add(powerupToAdd);
                    
                }
            }
        }

    }
    public void Remove(Powerup powerupToRemove)
    {
        removedPowerupsQue.Add(powerupToRemove);
        powerupToRemove.Remove(this);
        if(powerupToRemove.canStack == true && powerupToRemove.stack > 0)
        {
            powerupToRemove.stack -= 1;
        }
    }


    private void ApplyRemovePowerupsQue()
    {
        foreach (Powerup powerup in removedPowerupsQue)
        {
            if(powerup != null)
            {
                powerups.Remove(powerup);

            }
        }
    }


    public void DecrementPowerupTimer()
    {
        foreach(Powerup powerupToRemove in  powerups)
            {
                powerupToRemove.currentDuration -= Time.deltaTime;
                if(powerupToRemove.currentDuration <= 0)
                {
                Remove(powerupToRemove);
                }

            }
    }


}
