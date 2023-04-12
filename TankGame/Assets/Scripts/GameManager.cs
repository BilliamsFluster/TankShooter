using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;
    public List<GameObject> playerControllers;
    public List<GameObject> playerTanks;
    public int numberOfPlayers = 1;
    public bool mapOfTheDay = false, randomLevel = false;
    public int rows = 5, cols = 5, mapSeed = 0;
    public bool multiplayer = false;
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
        if(SceneManager.GetActiveScene().name != "Level")
        {
          ActivateMainMenuScreen();
          ToggleMultiplayer(multiplayer);
        }
        else
        {
            SpawnPlayers();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayers()
    {
        BoxCollider spawnArea = GetComponent<BoxCollider>();
        for(int i = 0; i < numberOfPlayers ; i ++)
        {
            if (spawnArea)
            {
                // Get the dimensions of the box collider
                Vector3 boxSize = spawnArea.bounds.size;

                // Calculate the minimum and maximum positions within the collider
                Vector3 minPosition = spawnArea.bounds.min;
                Vector3 maxPosition = spawnArea.bounds.max;

                Vector3 randomPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                                                            Random.Range(minPosition.y, maxPosition.y),
                                                            Random.Range(minPosition.z, maxPosition.z));


                newPawnObj = Instantiate(playerTanks[i], randomPosition, Quaternion.identity);
                GameObject newPlayerControllerObj = Instantiate(playerControllers[i], newPawnObj.transform.position, Quaternion.identity);


                Controller newController = newPlayerControllerObj.GetComponent<Controller>();

                Pawn newPawn = newPawnObj.GetComponent<Pawn>();
                TankPawn tankPawn = playerTanks[i].GetComponent<TankPawn>();
                if (tankPawn != null)
                {
                    newPawn.tankController = newController;
                }
                newController.pawn = newPawn;

            }
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

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit App");
    }
    public void OnMapOfTheDayChanged(bool val)
    {
        mapOfTheDay = val;
    }
    public void OnRandomLevelChanged(bool val)
    {
        randomLevel = val;
    }

    
    public void ToggleMultiplayer(bool val)
    {
        multiplayer = val;
        if(multiplayer)
        {
            numberOfPlayers = 2;
            Camera player1Camera = playerTanks[0].GetComponentInChildren<Camera>();
            Rect viewportRect = player1Camera.rect;
            viewportRect.width = 0.5f;
            player1Camera.rect = viewportRect;
        }
        else
        {
            numberOfPlayers = 1;
            Camera player1Camera = playerTanks[0].GetComponentInChildren<Camera>();
            Rect viewportRect = player1Camera.rect;
            viewportRect.width = 1;
            player1Camera.rect = viewportRect; // set the updated viewportRect to player1Camera
        }
    }
    public void ChangeMapSeed(string val)
    {
        mapSeed = int.Parse(val);
    }

    public void ChangeRows(string val)
    {
        rows = int.Parse(val);
    }
    public void ChangeCols(string val)
    {
        cols = int.Parse(val);
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
