using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    
    void Start()
    {
        Invoke("LoadNextScene", 4f);
    }

    // Update is called once per frame
    void LoadNextScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
