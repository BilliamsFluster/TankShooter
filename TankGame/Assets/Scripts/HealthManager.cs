using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public delegate void AIDeathEventHandler(GameObject aiGameObject);
    public static event AIDeathEventHandler OnAIDeath;
    public int deathScoreReward = 10;
    [SerializeField] HealthBar healthBar;



    public void TakeDamage(float dmg, GameObject instigator) // when we take damage 
    {
        if(health - dmg <=0)
        {
            health = 0;
            Death(instigator);
            Debug.Log("Object Dead");

        }
        else
        {
            health -= dmg;
            Debug.Log("Taken damage");
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void Death(GameObject instigator)
    {
        if (instigator == null)
        {
            Debug.LogError("Instigator is null.");
            return;
        }

        TankPawn tankPawn = instigator.GetComponent<TankPawn>();

        if (tankPawn != null)
        {
            if (tankPawn.tankController != null)
            {
                tankPawn.tankController.AddScore(deathScoreReward);
            }
            
        }
        healthBar.UpdateHealthBar(health, maxHealth);

        if (gameObject.tag == "Enemy")
        {
            if (OnAIDeath != null)
            {
                OnAIDeath(gameObject);
            }
            Destroy(gameObject);
        }
        else
        {
            GameManager.instance.ActivateGameOverScreen();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
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
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
