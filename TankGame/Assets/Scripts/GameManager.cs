using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;
    public GameObject playerController;
    public GameObject tank;
    public Transform playerSpawnTransform;
    public List<TankController> players;
    public List<AIController> enemies;

    [HideInInspector]
    public GameObject newPawnObj;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        players = new List<TankController>();
        enemies = new List<AIController>();
        //for(enemy: enemies)
        //{
        //    enemy.player = 
        //}
        
    }
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
       GameObject newPlayerControllerObj =  Instantiate(playerController, playerSpawnTransform.position, Quaternion.identity);
       newPawnObj = Instantiate(tank, playerSpawnTransform.position, Quaternion.identity);

       Controller newController = newPlayerControllerObj.GetComponent<Controller>();
       
       Pawn newPawn = newPawnObj.GetComponent<Pawn>();

       newController.pawn = newPawn;



    }
}
