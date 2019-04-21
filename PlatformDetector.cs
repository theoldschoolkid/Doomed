using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the platform comes in contact with this gameobject, New level is initialized
/// </summary>
public class PlatformDetector : MonoBehaviour
{

    SpawnGameObjects _spawnGameObjectsScript;

    private void Start()
    {
        _spawnGameObjectsScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnGameObjects>();
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "PlayerPlatform" && !GameManager.gm.gameIsOver)
        {
                 
            GameManager.gm.SpinWalls();
            GameManager.gm.ShrinkWalls();
            ResourceManager.Play(AudioClipName.LevelS,1f);

            Invoke("Spawn", 4.2f);                 // Spawn enemies after 4.2 seconds after the platform coes in contact with Marker

        }

    }

    void Spawn()
    {
        _spawnGameObjectsScript.SpawnObjects(); // Spawn enemies
        GameManager.gm.RunTimer = true;         // Resume timer   
    }

}
