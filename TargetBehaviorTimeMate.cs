using UnityEngine;
using System.Collections;

public class TargetBehaviorTimeMate : MonoBehaviour
{
    /// <summary>
    /// TimeMate Object beahvior script
    /// </summary>
    public int scoreAmount = 0;
	public float timeAmount = 15.0f;   // Time added when this is destroyed
    bool canFire = false;              // Bool to determine if the TimeMate is recharged
    float fireTimer = 0;               // Timer Counter
    float FireTdefault = 10f;          // Time need to recharge


    public GameObject FireSpawnPoint, parentPS, childPS;
    MeshRenderer face;                                          // The model rendering component
    Animator _animator;

    private void Start()
    {
        face = this.GetComponent<MeshRenderer>();             // store the model rendering component
        _animator = this.GetComponent<Animator>();    
    }


    void OnCollisionEnter (Collision newCollision)
	{
       
        if (GameManager.gm) {

			if (GameManager.gm.gameIsOver)
				return;
		}

        // check if the collion is from the projectile coming out of players gun and if this is recharged

        if (newCollision.gameObject.tag == "Projectile" && canFire == true)
        {
                Destroy(newCollision.gameObject);                               // destroy Projectile bullet
                _animator.SetBool("Activate", false);                       // Play recharging animation
                canFire = false;                                            // Disable until it is recharged
                childPS.SetActive(false);                                   
                parentPS.SetActive(true);                                    // Play recharging animation          
                face.enabled = false;                                       // Disable face rendering(Make face model disappear
                fireTimer = 0;                   
                ResourceManager.Play(AudioClipName.TimeMate, 1f);           // Play SFX  
                GameManager.gm.targetHit(scoreAmount, timeAmount);          // Add score and time 
        }
	}
 

    private void Update()
    {
        

        fireTimer += Time.deltaTime;           //caluculating the recharge timer

        if (fireTimer >= 2f && canFire == false)
        {
            childPS.SetActive(true);            // Enable recharging animation
            parentPS.SetActive(false);          // Disable glowing animation
           
        }

        if( fireTimer > FireTdefault)
        { 
            _animator.SetBool("Activate", true);          // Enable recharged animation
            face.enabled = true;                         // Enable face rendering
            childPS.SetActive(false);                   // Disable recharging animation
            canFire = true;                             
        }

    }

  
}
