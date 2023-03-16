using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{




    public AIController[] enemyTanks;
    public int enemiesToSpawn;

    void Start()
    {
        SpawnTankAI();
    }

    // Update is called once per frame
    void Update()
    {

    }




    void SpawnTankAI()
    {

        BoxCollider spawnBoxCollider = gameObject.GetComponent<BoxCollider>();

        // Get the dimensions of the box collider
        Vector3 boxSize = spawnBoxCollider.bounds.size;

        // Calculate the minimum and maximum positions within the collider
        Vector3 minPosition = spawnBoxCollider.bounds.min;
        Vector3 maxPosition = spawnBoxCollider.bounds.max;

        // if we have a tank to spawn
        if (enemyTanks.Length > 0)
        {

            // Spawn random objects within the collider

            for (int i = 0; i < enemiesToSpawn; i++)
            {

                Vector3 randomPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                                                     Random.Range(minPosition.y, maxPosition.y),
                                                     Random.Range(minPosition.z, maxPosition.z));

                Instantiate(enemyTanks[Random.Range(0, enemyTanks.Length - 1)], randomPosition, Quaternion.identity);
            }
        }



    }
}


   

