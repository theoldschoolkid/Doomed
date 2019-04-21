using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make enemies face towads the player Also helps in creating circular motion around the player
/// </summary>
public class LookAt : MonoBehaviour
{
    GameObject Player;
   
    void Start()
    {
         Player = GameObject.FindGameObjectWithTag("Player");
    }

   
    void Update()
    {
       transform.LookAt(Player.transform);
    }
}
