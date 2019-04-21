using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initialize the resource manager file
/// </summary>
public class GameInitializer : MonoBehaviour
{

    AudioSource aSource;

    private void Awake()
    {
        if(!ResourceManager.initialized)
        {
            aSource = gameObject.AddComponent<AudioSource>();
            ResourceManager.InitializeResources(aSource);
            DontDestroyOnLoad(this.gameObject); // Dont destroy this on load of another scene, this is inmitialized at main menu 
        }
        else
        {
            Destroy(this.gameObject); // If this is a duplicate then destroy this
        }
    }

}
