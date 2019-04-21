using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior script for the bullet generated from HellMate
/// </summary>
public class BulletForce : MonoBehaviour
{
    GameObject[] enemies;
    Rigidbody _rigidbody ;
    float x, y, z;
    int Target;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find the number of enemies at start
        Target = Random.Range(0, enemies.Length);             // Choose a random target
        _rigidbody = this.GetComponent<Rigidbody>();
        Invoke("DestroyBullet", 2f);                          // Destroy this bullet after two seconds


    }

   
    void Update()
    {
       // Find the number of enemies evey frame incase if any got destroyed by other bullet
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); 

        if (Target < enemies.Length)
        {
            if (enemies.Length == 0 || enemies[Target] == null) // if the target the bullet was tracking gets destroyed then skip this frame
            {            
                return;
            }

            // Shoot towards the enemy
            this.transform.position = Vector3.MoveTowards(this.transform.position, enemies[Target].transform.position, 1f); 

        }
        
    }

    void DestroyBullet()
    {
        if(_rigidbody.velocity == Vector3.zero) // If the bullet has no target
        {
            Destroy(this.gameObject);
        }
    }
}
