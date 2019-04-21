using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehavior : MonoBehaviour
{
    [SerializeField]
    int min, max , spinSpeed;

    float counter,timer;
    bool played = false;
    GameObject player;
    public GameObject fire;     // Fire effect around the enemy
   


    void Start()
    {
        counter = 0;
        timer = Random.Range(min, max);                        // A random time range between which the attack is triggered
        player = GameObject.FindGameObjectWithTag("Player");   // Find the target
    }

  
    void Update()
    {
       
        counter = counter + Time.deltaTime;
        if(counter > timer )
        {
            this.gameObject.GetComponentInParent<TargetMover>().enabled = false;    // Disable the horizontal movement
          //  fire.SetActive(true);                                                   // activate the fire effect
            SelfDestruct();                                                         // Initiate the attack
            if(!played)
            {
                ResourceManager.Play(AudioClipName.HellMate,2f); // Play audio once
                played = true;
            }
        }
    }

    void SelfDestruct()
    {
        // Rotate the object on its x axis
        this.gameObject.transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);  

        // Move towards the player to self destruct
        this.transform.parent.position = Vector3.MoveTowards(this.transform.parent.position, player.transform.position, 1f);  
    }


}
