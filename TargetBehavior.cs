using UnityEngine;
using System.Collections;

/// <summary>
/// Monster and spinner Enemy Behavior script
/// </summary>
public class TargetBehavior : MonoBehaviour
{	

	public int scoreAmount = 5;         // Same for both
	public float spinnerTimeAmount = -5.0f;   
    public float monsterTimeAmount = 0.0f;
    public float health = 2;                        // Health of the object 
    public float ProjectileHM_DamageAmount = 2f; // Damage amount receieved from the HellMate bullet
    public GameObject explosionPrefab,hitPrefab;
    

    void OnCollisionEnter (Collision newCollision)
	{
             
        if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}       

        if (newCollision.gameObject.tag == "Projectile" || newCollision.gameObject.tag == "ProjectileHM")
        {
           Destroy(newCollision.gameObject);

            if (newCollision.gameObject.tag == "Projectile")
            {
                ResourceManager.Play(AudioClipName.BulletImpact, 1f);
                health--;                                                               // If its projectile decrease the health amount by 1
                Instantiate(hitPrefab, this.transform.position, transform.rotation);   // Play hit animation
            }
            else
            {                
                health -= ProjectileHM_DamageAmount;  // If its ProjectileHM decrease the health amount by "ProjectileHM_DamageAmount"
            }


            if (health <= 0) // When enemy is killed
            {
                
                GameManager.gm.targetHit(scoreAmount, monsterTimeAmount);   // Add score and time to UI
                Instantiate(explosionPrefab, this.transform.position, transform.rotation); // Play explosion animation
                Destroy(this.gameObject);
            }

        }

        // For spinnerMonster type enemy
        if (newCollision.gameObject.tag == "Player")
        {
            Instantiate(hitPrefab);
            GameManager.gm.targetHit(0, spinnerTimeAmount); // -5 secs is deducted if player gets hit by spinner
            Destroy(this.gameObject); // Destroy after hit 
        }
    }    
   
}
