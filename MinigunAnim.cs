using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Initializes the Animation played when player runs out of time
/// </summary>
public class MinigunAnim : MonoBehaviour
{
    [SerializeField]
    GameObject[] PS;           // Particle systems that needs to be played

    [SerializeField]
    GameObject platformBreakPS, ironCyborg;  
    

    void Start()
    {
        ironCyborg.GetComponent<Animator>().SetBool("Activated", true);   // Play Iron Cyborgs Animations
        ironCyborg.GetComponent<Animator>().SetBool("Activated2", true);
        ResourceManager.Play(AudioClipName.Minigun, 0.5f);

     
        Invoke("ActivateGun", 0.7f);    // play minigun animation after 0.7secs
        Invoke("ActivatePS", 2.5f);       // play platform break animation after 2.5 secs
        Destroy(this.gameObject,3f);        // Destroy this gameobeject(to stop particle system playing)
        
    }

    void ActivateGun()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {

        for(int i=0; i< PS.Length; i++)
        {
            PS[i].SetActive(true);
            yield return new WaitForSeconds(0.025f); // Time difference between every particle system activating
        }
        
    }

    void ActivatePS()
    {
        platformBreakPS.SetActive(true);        
    }
}
