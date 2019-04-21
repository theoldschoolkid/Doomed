    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehavior: MonoBehaviour
{
    [SerializeField]
    GameObject DragonBulletPrefab , spawnPoint , hitPrefab;
    bool dead = false;
    Vector3 psOffset = new Vector3(0, 1, 0);

    [SerializeField]
     float ProjectileHM_DamageAmount = 0.5f; // Damage the Dragon receives when the projectile from hellmate(HM) hits it

    Animator _animator;
    GameObject Player;

    [SerializeField]
    float attackTimer = 7f;             // The time gap between dragon attacks

    float timer = 0f;                 // attackTimer counter

    [SerializeField]
    int timeAmount,scoreAmount;    // Time and score amount awarded to player when he kills this dragon

    Rigidbody _rigidbody;

    [SerializeField]
    float health = 4;          // Dragon health


    void Start()
    {
        //Store components of the gameobject at start 
        _animator = this.gameObject.GetComponent<Animator>();
        _rigidbody = this.GetComponent<Rigidbody>();
         Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // OnCollision of some object, check if the collided object's tag is either Projectile (Bullet from players gun) 
        // or ProjectileHM (Bullet from Hellmate)

        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "ProjectileHM")
        {
            Destroy(collision.gameObject);
            GameObject ps = Instantiate(hitPrefab);

            psOffset = this.transform.position;
            psOffset.y = this.transform.position.y + 8;

            ps.transform.position = psOffset;

            if (collision.gameObject.tag == "Projectile")
            {
                ResourceManager.Play(AudioClipName.BulletImpact, 1f); 
                health--;                                               // If its projectile decrease the health amount by 1
            }
            else
            {
                health -= ProjectileHM_DamageAmount; // If its ProjectileHM decrease the health amount by "ProjectileHM_DamageAmount"
            }
            
            if (health <= 0 && !dead) 
            {
                
                dead = true;
                GameManager.gm.targetHit(scoreAmount, timeAmount); // Add score and time to UI
                ResourceManager.Play(AudioClipName.DragonDeath, 1f); // Play dragon death sound
                _rigidbody.isKinematic = false;  // Turn off isKinematic and box collider so that it wont collide with floor when falling down
                this.GetComponent<BoxCollider>().enabled = false;
                _animator.SetTrigger("Death");  // Play death animation 
                Destroy(this.gameObject, 4f);   // and wait 4 seconds for dragon to fall down and then destroy
            }
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;    // calculate attack timer every frame
        if(timer > attackTimer && !dead)
        {
            _animator.SetTrigger("Attack"); 
            Invoke("Fire",0.65f);  // Fire after 0.65 seconds so that the face animation matches the attack
            timer = 0;  // reset attack time to 0
        }
    }

    void Fire()
    {
        ResourceManager.Play(AudioClipName.DragonAttack,1f);
        GameObject projectile = Instantiate(DragonBulletPrefab) as GameObject;
        projectile.transform.position = spawnPoint.transform.position; // Create Projectile and set its origin point to spawnPoint's position
    }
}
