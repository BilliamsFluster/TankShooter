using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WidgetManager : MonoBehaviour
{
    public GameObject TitleSccreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
    

    // Start is called before the first frame update
    void Start()
    {

        ActivateMainMenuScreen();

    }

    // Update is called once per frame
    void Update()
    {
        
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
       // gameOver = false;

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
    
    public void OpenLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Quit App");
    }
    public void OnMapOfTheDayChanged(bool val)
    {
        GameManager.instance.OnMapOfTheDayChanged(val);
    }
    public void OnRandomLevelChanged(bool val)
    {
        GameManager.instance.OnRandomLevelChanged(val);
    }
    public void ChangeMapSeed(string val)
    {
        GameManager.instance.ChangeMapSeed(val);
    }

    public void ChangeRows(string val)
    {
        GameManager.instance.ChangeRows(val);
    }
    public void ChangeCols(string val)
    {
        GameManager.instance.ChangeCols(val);
    }

    public void ToggleMultiplayer(bool val)
    {
        GameManager.instance.ToggleMultiplayer(val);
    }

}
