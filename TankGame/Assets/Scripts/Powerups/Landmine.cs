using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    // Start is called before the first frame update


    public float damageAmt = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthManager objectHealth = other.GetComponent<HealthManager>();
        if(objectHealth)
        {
            objectHealth.health -= damageAmt;
            Destroy(gameObject);
        }
    }
}
