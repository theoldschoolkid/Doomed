﻿using UnityEngine;
using System.Collections;

/// <summary>
/// HellMate Object beahvior script
/// </summary>
public class TargetBehaviorHellMate : MonoBehaviour
{	

	public int scoreAmount = 0;
	public float timeAmount = 0.0f;
    bool canFire = false;               // Bool to determine if the hellMate is recharged and ready to fire again
    float fireTimer = 0;                // Timer Counter
    float FireTdefault = 10f;           // Time need to recharge
    int bulletNumbers = 30;             // number of bullets the hellmate will spawn
	
	public GameObject explosionPrefab, FireSpawnPoint, bulletPrefab , particleS; // FireSpawnPoint -  where bullets are spawned



    void OnCollisionEnter (Collision newCollision)
	{
       
        if (GameManager.gm) {

			if (GameManager.gm.gameIsOver)
				return;
		}
        // if the collion is from the projectile coming out of players gun, there is chances that the bullet generated by this object
        // can hit itself hence we need to check if the projectile it came in contact is from the players gun

        if (newCollision.gameObject.tag == "Projectile" && canFire == true)  
        {                                                                   
            Destroy(newCollision.gameObject);                                  // destroy Projectile bullet

                canFire = false;                                             // Disable until it is recharged
                particleS.SetActive(false);                                 // Turn off animation
                fireTimer = 0;                                             // Reset recharge timer
                this.GetComponent<TargetMover>().spinSpeed = 2000;        //  Change the spin speed for effect(starts spinning rapidly)
                ResourceManager.Play(AudioClipName.HellMate, 1f);        // Play the audio
                ResourceManager.Play(AudioClipName.HellMate2, 1f);
                StartCoroutine( HellFire());                           // Start the bulletraining courotine
        }
	}

  
    IEnumerator HellFire()
    {
        Vector3 BulletSP = FireSpawnPoint.transform.position;   

        for (int i = 0; i < bulletNumbers; i++)
        {                        
                    // set a random position around(2) the BulletSpawn point for every bullet to spawn
                    BulletSP.x = Random.Range(FireSpawnPoint.transform.position.x + 2, FireSpawnPoint.transform.position.x - 2);
                    BulletSP.y = Random.Range(FireSpawnPoint.transform.position.y + 2, FireSpawnPoint.transform.position.y - 2);
                    BulletSP.z = Random.Range(FireSpawnPoint.transform.position.z + 2, FireSpawnPoint.transform.position.z - 2);
                    yield return new WaitForSeconds(0.02f); // Generate bullet with a gap of every 0.02 seconds
                    Instantiate(bulletPrefab, BulletSP, FireSpawnPoint.transform.rotation);    // Instantiate the bullet                              
        }

    }

    private void Update()
    {

        fireTimer += Time.deltaTime; //caluculating the recharge timer

        if(fireTimer >= 1f && canFire == false)
        {
            this.GetComponent<TargetMover>().spinSpeed = 200; // change the spin speed back to normal after 1 second (from 2000 to 200)
        }

        if( fireTimer > FireTdefault)  // Once the recharge time is met activate the particle system and make it ready to use again
        {
            particleS.SetActive(true); 
            canFire = true;          
        }



    }
}
