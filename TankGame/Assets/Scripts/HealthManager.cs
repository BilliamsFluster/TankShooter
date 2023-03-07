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
           

        }
        else
        {
            health -= dmg;
            Debug.Log("Taken damage");
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }


    public void Heal(float healthtoAdd)
    {
        if (health + healthtoAdd >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healthtoAdd;
        }
    }
}
