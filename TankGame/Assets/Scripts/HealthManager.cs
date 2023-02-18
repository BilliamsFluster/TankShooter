using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;

   public void TakeDamage(float dmg) // when we take damage 
    {
        if(health - dmg <=0)
        {
            health = 0;
            Death();
            Debug.Log("Object Dead");
            Destroy(gameObject);

        }
        else
        {
            health -= dmg;
            Debug.Log("Taken damage");
        }
    }

    public void Death()
    {

    }
}
