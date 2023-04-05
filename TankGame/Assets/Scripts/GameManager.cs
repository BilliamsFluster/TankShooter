using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;
    public GameObject playerController;
    public GameObject tank;
    public List<TankController> players;
    public List<AIController> enemies;

    [HideInInspector]
    public GameObject newPawnObj;

    //Game States

    public GameObject TitleSccreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;


    /* Sounds */
    public AudioClip impactSound;
    public AudioClip shotSound;
    public  AudioSource impactSource;
    public  AudioSource shotSource;
    public float shotSoundRange = 25f;
    public float impactSoundRange = 10f;

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

        //sounds
        impactSource = (AudioSource)gameObject.AddComponent<AudioSource>();
        impactSource.clip = impactSound;

        shotSource = (AudioSource)gameObject.AddComponent<AudioSource>();
        shotSource.clip = shotSound;


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
        BoxCollider spawnArea = GetComponent<BoxCollider>();

        if(spawnArea)
        {
            // Get the dimensions of the box collider
            Vector3 boxSize = spawnArea.bounds.size;

            // Calculate the minimum and maximum positions within the collider
            Vector3 minPosition = spawnArea.bounds.min;
            Vector3 maxPosition = spawnArea.bounds.max;

            Vector3 randomPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                                                        Random.Range(minPosition.y, maxPosition.y),
                                                        Random.Range(minPosition.z, maxPosition.z));


            newPawnObj = Instantiate(tank, randomPosition, Quaternion.identity);
            GameObject newPlayerControllerObj = Instantiate(playerController,newPawnObj.transform.position, Quaternion.identity);
            

            Controller newController = newPlayerControllerObj.GetComponent<Controller>();

            Pawn newPawn = newPawnObj.GetComponent<Pawn>();
            TankPawn tankPawn = tank.GetComponent<TankPawn>();
            if(tankPawn !=null)
            {
                newPawn.tankController = newController;
            }
            newController.pawn = newPawn;

        }



      



    }


    public void PlayTankImpactSound()
    {
        if (impactSound != null)
        {
            impactSource.PlayOneShot(impactSound);

            Debug.Log(impactSound.ToString());
            
        }
    }

    public void PlayTankShotSound()
    {
        if (shotSound != null)
        {
            shotSource.PlayOneShot(shotSound);
            
            
            Debug.Log(shotSound.ToString());

        }

    }

    private void DeactivateAllStates()
    {
        TitleSccreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    public void ActivateTitleScreen()
    {
        DeactivateAllStates();
        TitleSccreenStateObject.SetActive(true);
    }
    public void ActivateMainMenuScreen()
    {
        DeactivateAllStates();
        MainMenuStateObject.SetActive(true);

    }
    public void ActivateOptionsScreen()
    {
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);

    }
    public void ActivateCreditsScreen()
    {
        DeactivateAllStates();
        CreditsScreenStateObject.SetActive(true);

    }
    public void ActivateGameplayStateObject()
    {
        DeactivateAllStates();
        GameplayStateObject.SetActive(true);

    }
    public void ActivateGameOverScreen()
    {
        DeactivateAllStates();
        GameOverScreenStateObject.SetActive(true);

    }

}
