using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;
    public GameObject Platform;

	// public variables
	public int score;
    AudioSource _timerAudio;
	
	public int beatLevelScore=0;

	public float startTime;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;
    public GameObject refreshLevel;
    RefrestLevel refrestLevelScript;
  

	public bool gameIsOver = false;
    public Light DirLight;              // Direction light on the scene
    

    public GameObject Level,shoot;   // Level UI text, shoot animation

    private float currentTime;
    bool runTimer = true;
    string timeString;

    public int levelCounter = 1;
    int dotLocation;
    int i;

    GameObject[] Enemies;

    public GameObject Walls;                                // ------Change to Lists------
    Transform[] cWalls;
    Transform[] AllChildWalls = new Transform[100];
    Spin[] spinScript = new Spin[76];
    Transform[] childTransforms = new Transform[76];

    public bool spawned = false;

    int k = 0;

    private void Awake()
    {

        // Store all the vertical walls on 4 sides inside a variable 

        int children = Walls.transform.childCount;
        cWalls = new Transform[children];
        for (int i = 0; i < children; ++i)
        {
            cWalls[i] = Walls.transform.GetChild(i);        // store the 4 childwalls from Parent Wall 
        }

        for (int i = 0; i < children; ++i)
        {
            int Cchildren = cWalls[i].transform.childCount;     // Extract the childwalls from the parent cWalls

            for (int j = 0; j < Cchildren; j++)
            {
                AllChildWalls[k] = cWalls[i].transform.GetChild(j);
                k++;
            }
        }

        for (int q = 0; q < 76; q++)
        {
            spinScript[q] = AllChildWalls[q].GetComponent<Spin>(); // Get the spin script component from all the child walls
        }

        for (int q = 0; q < 76; q++)
        {
            childTransforms[q] = AllChildWalls[q].GetComponent<Transform>(); // Get the Transform component from all the child walls
        }

    }


    void Start () {

        _timerAudio = this.GetComponent<AudioSource>();
        ResourceManager.Play(AudioClipName.LevelS, 1f);   // Play opening sound at the start of the level

        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();  
       		
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(false);     // Turn off the outline at start
        
        score = beatLevelScore;
        currentTime = startTime;
        LockCursor(true);                            // Lock the cursor

        mainScoreDisplay.text = "Score To Beat : " + score;

        refrestLevelScript = refreshLevel.GetComponent<RefrestLevel>();

    }

	
	void Update () {

        Physics.IgnoreLayerCollision(8, 9, true);    // Ignore collision between Player and the enemy all time

        if  (score <= 0)        // This means player needs to proceed to next level
        {
         
            levelCounter++;

            Enemies = GameObject.FindGameObjectsWithTag("Enemy") ;     // Destroy all the remaininig enemies in the current level
            foreach(GameObject obj in Enemies)  {
                Destroy(obj);                                         
            }

            StartCoroutine(ChangeSkybox());
            RotateDirLight();
         
            _timerAudio.enabled = false;    // If end timer music is playing disable it

            refrestLevelScript.Initialize();
            refrestLevelScript.refreshSet = true;        

            

            if (currentTime < 11)
            {
                currentTime = 11;   // If current time is less than 11 when changing level, make it as 11
            }


            runTimer = false;   // Stop the timer when the level is changing

            beatLevelScore += 10;       // Increase score to beat for next level
            score = beatLevelScore;    

            mainScoreDisplay.text = "Score to beat: " + score; // Set the score in the UI to beatscorelevel
            Level.SetActive (true);                           // Activate Level UI text and update it
            Level.GetComponentInChildren<Text>().text = "Level " + levelCounter; 

            ResourceManager.Play(AudioClipName.Lift, 1f); 

            Invoke("DisableLevelDisplay", 2f);  //Function to disable Level UI text after 2 seconds

            ExpandWalls(); 

        }

        if(runTimer == true && !gameIsOver)   // Start the timer once the level is changed
        Timer();
       
    }

    IEnumerator ChangeSkybox()
    {
        // Level Change Effect
         i = 0;
        while (i <= 16)
        {
            int Z = Random.Range(0, 10);
            switch (Z)
            {
                case 0:
                    ResourceManager.ChangeSkybox(MaterialName.ETBS);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 1:
                    ResourceManager.ChangeSkybox(MaterialName.EBumper);                    
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    
                    break;

                case 2:
                    ResourceManager.ChangeSkybox(MaterialName.EGreen);
                    i++;
                    yield return new WaitForSeconds(0.3f);                    
                    break;

                case 3:
                    ResourceManager.ChangeSkybox(MaterialName.ENavyGrid);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 4:
                    ResourceManager.ChangeSkybox(MaterialName.EPickup);
                    i++;
                    yield return new WaitForSeconds(0.3f);

                    break;

                case 5:
                    ResourceManager.ChangeSkybox(MaterialName.EPinkSmooth);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 6:
                    ResourceManager.ChangeSkybox(MaterialName.ERedDust);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 7:
                    ResourceManager.ChangeSkybox(MaterialName.EPlatform);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 8:
                    ResourceManager.ChangeSkybox(MaterialName.ERedYello);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 9:
                    ResourceManager.ChangeSkybox(MaterialName.EPurple);
                    i++;
                    yield return new WaitForSeconds(0.3f);
                    break;



            }
        }

       //Set Current Level to random skybox 
        int x = Random.Range(0, 7);

        switch (x)
        {
            case 0:
                ResourceManager.ChangeSkybox(MaterialName.Bonus);               
                break;

            case 1:
                ResourceManager.ChangeSkybox(MaterialName.Bumper);              
                break;

            case 2:
                ResourceManager.ChangeSkybox(MaterialName.Green);
                break;

            case 3:
                ResourceManager.ChangeSkybox(MaterialName.RedDust);
                break;

            case 4:
                ResourceManager.ChangeSkybox(MaterialName.Platform);
                break;

            case 5:
                ResourceManager.ChangeSkybox(MaterialName.TBS);
                break;

            case 6:
                ResourceManager.ChangeSkybox(MaterialName.DarkBlue);
                break;

        }

        print(Camera.main.GetComponent<Skybox>().material);

    }

    public bool RunTimer
    {
        set { runTimer = value; }
    }

    void Timer()  // Manage the UI timer text
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 11f)              // When the timer reaches 11 turn on the TimerAudioEffect
            _timerAudio.enabled = true;
        else
            _timerAudio.enabled = false;

        // Filter to display 1 number after "." in the timer UI text

        timeString = currentTime.ToString();
        dotLocation = timeString.IndexOf(".");
        mainTimerDisplay.text = timeString.Substring(0, dotLocation + 2);

        if (currentTime <= 0 && !gameIsOver )
        {
            EndGame();
        }
    }

    // Call this function when timer runs out
	void EndGame() {

        shoot.SetActive(true);  // Enable shoot animation on minigun attached to spaceship
        gameIsOver = true;
        Level.SetActive(true);       // Display level

        mainTimerDisplay.text = "GAME OVER";	

		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in Enemies)
        {
            Destroy(obj);
        }

        Invoke("EndGameScript", 2f);
        Invoke("MainMenu", 6f);
    }

    void EndGameScript()
    {
        Platform.SetActive(false);  // Disable the platform player is standing on        
    }

    void MainMenu()
    {
        LockCursor(false);                       // Enable mouse and load Main menu
        SceneManager.LoadScene("Main Menu");
    }


    public void targetHit (int scoreAmount, float timeAmount) // This function is called by enemies to set time and score amount
	{
		
		score += scoreAmount;
		mainScoreDisplay.text = "Score to beat : " +score.ToString ();
   
		currentTime += timeAmount;  // When you hit the time goblin add time else when you get attacked subtract time
				
		if (currentTime < 0)
			currentTime = 0.0f;

	}
	

    void DisableLevelDisplay()
    {
        Level.SetActive(false);
    }


    #region LightEffectDuringLevelChange

    void RotateDirLight()
    {
        DirLight.GetComponent<TargetMover>().enabled = true;
        Invoke("DisableSpinning", 4f);
    }
    void DisableSpinning()
    {
        DirLight.GetComponent<TargetMover>().enabled = false;
    }

    #endregion


    #region WallSpinEffectDuringLevelChange

    //Activate Spin wall script for 4 seconds using courotine(to give animated effect) and deactivate it 

    public void SpinWalls()
    {
        foreach (Spin sc in spinScript) 
        {
            sc.enabled = true;
        }
        Invoke("StopSpinWalls", 4f);  
    }

    public void StopSpinWalls()
    {
        foreach (Spin sc in spinScript)
        {
            sc.enabled = false;
        }

    }

    #endregion



    #region WallSizeEffectDuringLevelChange

   
    public void ExpandWalls() // Expand scale of wall until its size is 2.8 from 1.5 using courotine
    {
        foreach (Transform CT in childTransforms)  
        {
            StartCoroutine(Expand(CT));
        }

    }

    IEnumerator Expand(Transform ct)
    {
        while (ct.localScale.x < 2.8f)
        {
            ct.localScale += new Vector3(0.1f, 0, 0); // Expand 0.1 value of wall every 0.1 seconds
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShrinkWalls()  // Shrink scale of wall until its size is back to normal
    {
        foreach (Transform CT in childTransforms)
        {
            StartCoroutine(Shrink(CT));
        }

    }

    IEnumerator Shrink(Transform ct)
    {
        while (ct.localScale.x > 1.5f)
        {
            ct.localScale += new Vector3(-0.1f, 0, 0);  // Shrink 0.1 value of wall every 0.1 seconds
            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion


    // function to enable and disable cursor
    private void LockCursor(bool isLocked) 
    {
        if (isLocked)
        {
            // make the mouse pointer invisible
            Cursor.visible = false;

            // lock the mouse pointer within the game area
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // make the mouse pointer visible
            Cursor.visible = true;

            // unlock the mouse pointer so player can click on other windows
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
