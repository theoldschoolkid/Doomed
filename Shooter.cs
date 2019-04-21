using UnityEngine;
using System.Collections;

/// <summary>
/// Create and launch bullet
/// </summary>
public class Shooter : MonoBehaviour {
    
	public GameObject projectile;
	public float power;
    Vector3 bulletPos;
    int offset = 8;

	void Update () {
		
		if (Input.GetButtonDown("Fire1"))
		{	
			
			if (projectile)
			{				
                // Spawn bullet a little away from the center(offset)
				GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward * offset, transform.rotation) as GameObject;
				
				if (!newProjectile.GetComponent<Rigidbody>()) 
				{
					newProjectile.AddComponent<Rigidbody>(); //If it does not have a rigid body add a rigidbody componenet
				}
				
				newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange); // launch the projectile
                ResourceManager.Play(AudioClipName.GunEffect, 0.5f);				
			}
		}
	}
}
