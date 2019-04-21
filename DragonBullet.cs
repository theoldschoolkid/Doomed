using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FireBall projectile spit from the dragon 
/// </summary>
public class DragonBullet : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    int timeAmount = -2;  // Amount of time that will be deducted when the projectile hits the player
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Store player reference
    }

    
    void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 3f); //shoot projectile towards player
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collided object is player 
        if(other.gameObject.tag == "Player")
        {
            GameManager.gm.targetHit(0, timeAmount); // Then deduct timeamount from UI
            Destroy(this.gameObject);
        }

        // If the player shoots this projectile when its moving towards him then destroy this projectile
        if (other.gameObject.tag == "Projectile")
        {           
            Destroy(this.gameObject);
        }
    }
}
