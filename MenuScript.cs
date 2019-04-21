using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The respective functions run when the respective buttons are pressed in the main menu
/// </summary>

public class MenuScript : MonoBehaviour
{

    [SerializeField]
    GameObject  MainMenu, HelpMenu, HelpMenuObjects, credits; 
    bool move = false;
    
    
    public void NewGame()
    {
      //  AudioManager.Play(AudioName.MenuSelect);
       
        SceneManager.LoadScene("Intro");  // Load intro video scene
    }

    public void HoverOver()
    {
      //  AudioManager.Play(AudioName.Hover2);
    }


    public void Quit()
    {
      //  AudioManager.Play(AudioName.MenuSelect);
        Application.Quit();
    }

    public void Help()
    {
    //    AudioManager.Play(AudioName.MenuSelect);
        
        HelpMenu.SetActive(true);            // Disable MainMenu Option when Help button is clicked and activate help menu options
        HelpMenuObjects.SetActive(true);
        MainMenu.SetActive(false);

    }

    public void Credits()
    {
        //    AudioManager.Play(AudioName.MenuSelect);

        credits.SetActive(true);            // Disable MainMenu Option when Help button is clicked and activate help menu options
        MainMenu.SetActive(false);

    }

    public void BackCredits()
    {     
        credits.SetActive(false);            
        MainMenu.SetActive(true);
    }

    public void Back()
    {
    //    AudioManager.Play(AudioName.Back);     
        HelpMenu.SetActive(false);           // Disable HelpMenu Option when Back button is clicked and activate Main menu options
        HelpMenuObjects.SetActive(false);
        MainMenu.SetActive(true);      
    }

}



