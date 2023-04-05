using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    /*variables */
    public float bulletLife = 3f;
    public float bulletDamage = 5f;

    
    [HideInInspector]
    public Vector3 startLocation;

    [HideInInspector]
    public Vector3 hitLocation;

    [HideInInspector]
    public GameObject bulletOwner;




    void Start()
    {
        GameManager.instance.PlayTankShotSound(); // play a shot sound
        startLocation = transform.position;
        BulletSoundEnabled();
        Invoke("BulletSoundDisabled", .01f);

    }
    void Awake() // object lifetime
    {
        Destroy(gameObject, bulletLife); // destroy bullet after 

    }

    void OnCollisionEnter(Collision other) // when we hit something
    {
        DealDamage(other.gameObject);
        BulletSoundEnabled();
        Invoke("BulletSoundDisabled", .1f);
        hitLocation = transform.position;
    }

    void BulletSoundEnabled()
    {
        GameObject bulletObj = gameObject;
        if(bulletObj)
        {
            bulletObj.layer = LayerMask.NameToLayer("whatIsSound"); // make the bullet sound available
            

        }

    }
    void BulletSoundDisabled()
    {
        GameObject bulletObj = gameObject;
        if (bulletObj)
        {
            bulletObj.layer = LayerMask.NameToLayer("Default"); // make the bullet sound undetectable

        }
    }
    

    public void DealDamage(GameObject target) // deals damage to a gameobject
    {
        if(target.tag != "Bullet")
        {
            var HealthManager = target.GetComponent<HealthManager>();
            if (HealthManager != null) // does the object have a health manager
            {
                HealthManager.TakeDamage(bulletDamage,bulletOwner); // if it does subtract its health
                
                GameManager.instance.PlayTankImpactSound();
            }
            Destroy(gameObject);
        }
        if(target.tag == "Enemy")
        {
            AIController enemyRef = target.GetComponent<AIController>(); // get a reference to the hit enemy

            if(enemyRef != null) // check if the enemy is valid
            {
                var enemyHealthManager = enemyRef.GetComponent<HealthManager>(); // get the health component off of the enemy
                if(enemyHealthManager != null) // make sure the enemy's health component is valid
                {
                    if(enemyHealthManager.health - bulletDamage <= 50f) // if the enemy is below 50 health
                    {
                        enemyRef.isAggressive = true; // the enemy is now aggressive
                        enemyRef.Aggressive();
                    }
                    else // if the enemy is not  below 50 health
                    {
                        enemyRef.isAggressive = false; // the enemy is now passive
                        enemyRef.Passive();
                    }
                }
            }
        }
       
    }
}
