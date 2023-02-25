using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    /* Variables */
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float coolDownTime = 1f;
    private float lastShot;
    
    
    void Start()
    {
        
        
    }
    void Awake()
    {
        
    }
    void Update()
    {
      
    }

    public void ShootBullet()
    {
        if(gameObject.tag != "Enemy")
        {
            if (Time.time - lastShot < coolDownTime) // if we cant shoot than just return 
            {
                return;
            }
            lastShot = Time.time; // reset the timer
        }
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); // spawn bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse); // add force to the bullet to update its position.
        

        
    }

    
}
