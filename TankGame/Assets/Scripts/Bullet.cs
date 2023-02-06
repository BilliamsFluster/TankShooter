using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    /*variables */
    public float bulletLife = 3f;
    public float bulletDamage = 5f;

    
    
    void Awake() // object lifetime
    {
        Destroy(gameObject, bulletLife);
    }

    void OnTriggerEnter(Collider other) // when we hit something
    {
        DealDamage(other.gameObject);
        Debug.Log("Hit");
        Destroy(gameObject);

    }

    public void DealDamage(GameObject target) // deals damage to a gameobject
    {
        var HealthManager = target.GetComponent<HealthManager>(); 
        if (HealthManager != null) // does the object have a health manager
        {
            HealthManager.TakeDamage(bulletDamage); // if it does subtract its health
        }
    }
}
