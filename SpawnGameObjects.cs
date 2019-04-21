using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{
    // public variables

    public GameObject healthSpawnPoint;
    
    // 2 sets of minimun and manximun range of spawn area
    // 2 sets are defined to avoid spawning of enemies directly above the player ( gap from 10 to -10 in both x and z axis )

	 float xMinRange1 = 90.0f;
	 float xMaxRange1 = 10.0f;
	 float yMinRange1 = 8.0f;
	 float yMaxRange1 = 15.0f;
	 float zMinRange1 = 90.0f;
	 float zMaxRange1 = 10.0f;

     float xMinRange2 = -90.0f;
     float xMaxRange2 = -10.0f;
     float yMinRange2 = 8.0f;
     float yMaxRange2 = 15.0f;
     float zMinRange2 = -90.0f;
     float zMaxRange2 = -10.0f;

    public GameObject redDragon,Monster,SpinnerMonster;  //Enemy gameobjects to be spawned
 
    // Spawn points for enemies
    Vector3 monsterSP1;
    Vector3 monsterSP2;
    Vector3 dragonSP;
 
    // Number of enemies to be spawed, this will be increased as the level increases
    int monstersCount = 3;
    int spinnerMonstersCount = 1;
    int dragonCount = 1;  

	
	void Start ()
	{
         Invoke("SpawnObjects",4f);  // At the start of the game wait 4 seconds before spawning first wave of enemy
    }
			
	public void SpawnObjects() 
	{
        if (GameManager.gm)
        {
            if (GameManager.gm.gameIsOver)
                return;
        }


      

        // 2 monster type enemies are spawned in one loop, one at spawnpoint1 and another at spawnpoint 2

        for (int i=0; i < monstersCount/2 ;i++)  
        {
                
                 monsterSP1.x = Random.Range(this.transform.position.x + xMinRange1, this.transform.position.x + xMaxRange1); 
                 monsterSP1.y = Random.Range(this.transform.position.y + yMinRange1, this.transform.position.y + yMaxRange1);
                 monsterSP1.z = Random.Range(this.transform.position.z + zMinRange1, this.transform.position.z + zMaxRange1);

                 monsterSP2.x = Random.Range(this.transform.position.x + xMinRange2, this.transform.position.x + xMaxRange2);
                 monsterSP2.y = Random.Range(this.transform.position.y + yMinRange2, this.transform.position.y + yMaxRange2);
                 monsterSP2.z = Random.Range(this.transform.position.z + zMinRange2, this.transform.position.z + zMaxRange2);

                 Instantiate(Monster, monsterSP1, transform.rotation);
                 Instantiate(Monster, monsterSP2, transform.rotation);
        }


        // 2 spinnerMonsters type enemies are spawned in one loop, one at spawnpoint1 and another at spawnpoint 2

        for (int i = 0; i < spinnerMonstersCount / 2; i++) 
        {

            monsterSP1.x = Random.Range(this.transform.position.x + xMinRange1, this.transform.position.x + xMaxRange1);
            monsterSP1.y = Random.Range(this.transform.position.y + yMinRange1, this.transform.position.y + yMaxRange1);
            monsterSP1.z = Random.Range(this.transform.position.z + zMinRange1, this.transform.position.z + zMaxRange1);

            monsterSP2.x = Random.Range(this.transform.position.x + xMinRange2, this.transform.position.x + xMaxRange2);
            monsterSP2.y = Random.Range(this.transform.position.y + yMinRange2, this.transform.position.y + yMaxRange2);
            monsterSP2.z = Random.Range(this.transform.position.z + zMinRange2, this.transform.position.z + zMaxRange2);

            Instantiate(SpinnerMonster, monsterSP1, transform.rotation);
            Instantiate(SpinnerMonster, monsterSP2, transform.rotation);
        }

       
        StartCoroutine(SpawnDragon()); // Spawn dragons with time intervals in between so that they do not attack at the same time


        // Increse the dragon count every 3 levels and decrease monstor count to balace the game

        if (GameManager.gm.levelCounter % 3 == 0)
        {
            dragonCount++;
            monstersCount -= 2;
        }
        else
        {
            monstersCount += 2;
            spinnerMonstersCount++;
        }

    }


    IEnumerator SpawnDragon()
    {

        for (int i = 0; i < dragonCount; i++)
        {
            dragonSP.x = Random.Range(this.transform.position.x + 80, this.transform.position.x +30 );
            dragonSP.y = Random.Range(this.transform.position.y + 15, this.transform.position.y + 30);
            dragonSP.z = Random.Range(this.transform.position.z + 80, this.transform.position.z + 30);
            Instantiate(redDragon, dragonSP, transform.rotation);            
            yield return new WaitForSeconds(2f);
        }
    }
        
}
