using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// user story 4 --> Game Over Exit() --> Credit: Marc-André Larouche
    /// user story 7 --> PlayerPrefs Code --> Credit: Marc-André Larouche
    /// </summary>

    // Levels manager
    private int curLevel = 0; // This is for my current level, so i can "find myself"

    [SerializeField] private GameObject[] level; // List of all levels
    private GameObject currentBoard; // This is the current level, you have it saved and them you delet it

    // ActiveFire
    private int activeFire = 0; // My quantity of fires will be 0 in the beginning

    // For user story 2
    [SerializeField] private int preventionScore;
    [SerializeField] private Text fires;
    [SerializeField] private Text txtTimer; // I need txt in the beginning otherwise it does not work
    private float timePassed = 0;
    private int firesNeverActive = 14;
    private float startingTime = 0; // Start time
    public float playerPoints = 0;
    [SerializeField] private GameObject endLevelScreen; // I'm going to use for user story 3

    // User story 3
    [SerializeField] private int pointsPerLevel = 200; // If i add levels i can increment it depending of the level
    [SerializeField] private int scoreForTime = 50;
    private bool victoryManager;

    // User story 4 --> Damage
    private float damage = 0;
    private float penaltyPoints;
    [SerializeField] private float damageSpeed = 0.5f;

    //User story 4 --> Game Over screen
    [SerializeField] private Button exitButton;

    // User story 5
    private int waterScore = 1000;
    [HideInInspector] public float waterStart;
    public float waterAmount;
    private float waterPoints;
    private float waterPercentage;
    
<<<<<<< HEAD
    // User story 7
=======
    //user story 7
>>>>>>> upstream/main
    private int bestScore = 0; //best score from the registry

    // Public GameObject restartButton;
    [SerializeField] private Button restartButton;
    public string sceneToReload = "FireFighter";

    // Variables for point system
    private float totalPoints;
    public int averageTime; // This is for my average time in each level
    private float bonusPoints;
    private float timePoints;
    private float endingLevelPoints;

    // Txt
    [SerializeField] private Text txtVictory;
    [SerializeField] private Text txtTotalPoints;
    [SerializeField] private Text txtPointsLevelPassed;
    [SerializeField] private Text txtbonusPoints;
    [SerializeField] private Text txttimePoints;
    [SerializeField] private Text txtDamage;
    [SerializeField] private Text txtPenaltyPoints;
    [SerializeField] private Text txtExitButton;
    [SerializeField] private Text txtRestartButton;
    [SerializeField] public Text txtWater;
    [SerializeField] private Text txtPointsWater;
    [SerializeField] private Text txtBestScore;
<<<<<<< HEAD
    [SerializeField] private Text txtNextLevelButton;
    
    // Referencing my singleton
    public static GameManager instance = null; 
    
    // Singleton
=======

    //Singleton
    public static GameManager instance = null; // referencing my singleton

>>>>>>> upstream/main
    private void Awake()
    {
        // Singleton implementation
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

<<<<<<< HEAD
    // Level loading
=======
    //level loading

>>>>>>> upstream/main
    public void LoadLevel()
    {
        Invoke(nameof(Initiate), 0); //initialize level to make sure everything, see more about this

        if (currentBoard) //if i have a level loaded it will destroy it 
        {
            Destroy(currentBoard);
        }

        activeFire = 0; // to be sure if the counter panel is empty

        currentBoard = Instantiate(level[curLevel]);
        Time.timeScale = 1;
        startingTime = Time.time;
        endLevelScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    // Water function
    public void DecreaseWater(float amount)
    {
        waterAmount -= amount;
        waterPercentage = (waterAmount * 100) / waterStart;
        if (waterPercentage < 0)
        {
            FinishingLevel(false);
        }
    }

<<<<<<< HEAD
    // Quit Button
    public void QuitButton()
=======
    //exit function
    public void Exit()
>>>>>>> upstream/main
    {
        Application.Quit();
    }

    // Restart Button
    public void RestartButton()
    {
        if (victoryManager)
        {
            curLevel--;
        }
        
        LoadLevel();
    }
    
    // Button for go to main menu
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

<<<<<<< HEAD
    // Increase Fire Counter
=======
    //button for go to main menu
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

>>>>>>> upstream/main
    public void MoreFire()
    {
        activeFire++; //it will add more fires
        firesNeverActive--;
    }

<<<<<<< HEAD
    private void AddPoints() // Saving the player points
=======
    private void AddPoints() //Saving the player points
>>>>>>> upstream/main
    {
        if (victoryManager)
        {
            endingLevelPoints = pointsPerLevel;
            playerPoints += endingLevelPoints;
            Debug.Log("Ending Level Points: " + endingLevelPoints);
        }
        else
        {
            endingLevelPoints = 0;
            playerPoints += endingLevelPoints;
        }

        //adding all the points
        playerPoints += pointsPerLevel;
        Debug.Log("Points: " + pointsPerLevel); //i need this so i can see my points as a player

        // time points
        timePoints = ((averageTime / timePassed) * scoreForTime);
        playerPoints += timePoints;
        Debug.Log("Time Points: " + timePoints + "Time Passed: " + timePassed + "average time: " + averageTime + "score for time: " + scoreForTime);

        //bonus points
        bonusPoints = ((14 - firesNeverActive) * preventionScore); //teachers recommendation for the user story 2
        playerPoints += bonusPoints;
        Debug.Log("Bonus Points: " + bonusPoints);

        //penalty points
        // Add penalty points
        penaltyPoints = (10 * damage);
        playerPoints -= penaltyPoints;
        Debug.Log("Penalty Points: " + penaltyPoints);

        //water Points
        waterPoints = ((int) waterPercentage * waterScore);
        playerPoints += waterPoints; //(1000 pts/1%) . (waterLeft * scoreWater)
        Debug.Log("Water Points: " + waterPoints);

        //teacher's requirement, this is for my playerPoints never be less than 0
        if (playerPoints < 0)
        {
            playerPoints = 0;
        }
        
        //PlayerPrefs
        if (playerPoints > bestScore)
        {
            PlayerPrefs.SetFloat("Score",playerPoints);
            PlayerPrefs.Save();
        }


        //just to see if it's ok
        Debug.Log("Total of Points: " + playerPoints.ToString("000"));

    }

    public List<GameObject> LightUpFiresRandomly(List<GameObject> notUsedFire, int i)
    {
        //i'm lightning up a fire so i can remove it from my list
        notUsedFire[i].SetActive(true);

        // i'm removing it from my list
        notUsedFire.Remove(notUsedFire[i]);
        Debug.Log("lighting up a fire");
        return notUsedFire;
    }

    // Checking box
    private void Initiate()
    {
        GameObject[] checkbox = GameObject.FindGameObjectsWithTag("Checkbox"); //it will find my checkbox in unity
        damage = 0; //if i don't do this i will play another game with trash points

        //it sets all the check boxes as disabled  
        foreach (var check in checkbox)
        {
            check.SetActive(false);
        }
        
        waterPercentage = 100f;
    }

<<<<<<< HEAD
    // Show Victory Screen
    private void Victorytxt()
=======
    public void Victorytxt()
>>>>>>> upstream/main
    {
        txtVictory.text = "Congratulations, You Just Won The Level!!!";
        txtPointsLevelPassed.text = "Level Points: + " + endingLevelPoints;
        txttimePoints.text = "Time Points: " + timePoints;
        txtbonusPoints.text = "Bonus Points: " + bonusPoints;
        txtTotalPoints.text = "Final Score: " + playerPoints;
        txtPenaltyPoints.text = "Penalty Points: - " + penaltyPoints;
        txtPointsWater.text = "Water Points: " + waterPoints;
        txtExitButton.text = "Quit";
<<<<<<< HEAD
        txtNextLevelButton.text = "Next Level";
        GameObject.FindGameObjectWithTag("NextLevel").SetActive(true);
        GameObject.FindGameObjectWithTag("MainMenu").SetActive(false);
=======
>>>>>>> upstream/main
        if (playerPoints > bestScore)
        {
            txtBestScore.text = "[HiGH SCORE] ";
        }
        else
        {
            txtBestScore.text = "";
        }
<<<<<<< HEAD
    }
    
    // Show Defeat Screen
    private void Defeattxt()//it will show my defeat menu and i will use this to load in the script for scene exit menu
=======
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void Defeattxt()//it will show my defeat menu and i will use this to load in the script for scene exit menu
>>>>>>> upstream/main
    {
        txtVictory.text = "Defeated! Try again!";
        txtPointsLevelPassed.text = "Level Points: + " + endingLevelPoints;
        txttimePoints.text = "Time Points: " + timePoints;
        txtbonusPoints.text = "Bonus Points: " + bonusPoints;
        txtTotalPoints.text = "Final Score: " + playerPoints;
        txtPenaltyPoints.text = "Penalty Points: - " + penaltyPoints;
        txtPointsWater.text = "Water Points: " + waterPoints;
<<<<<<< HEAD
        txtExitButton.text = "Main Menu";
        GameObject.FindGameObjectWithTag("NextLevel").SetActive(false);
        GameObject.FindGameObjectWithTag("MainMenu").SetActive(true);
=======
        txtExitButton.text = "Quit";
>>>>>>> upstream/main
        if (playerPoints < bestScore)
        {
            txtBestScore.text = "";
        }
<<<<<<< HEAD
    }
    
    // Ends the level
    private void FinishingLevel(bool victory)
=======
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void FinishingLevel(bool victory) //my gameover is in here too,function Death
>>>>>>> upstream/main
    {
        
        AddPoints();
        endLevelScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //pausing
        Time.timeScale = 0;
        if (victory)
        {
<<<<<<< HEAD
            curLevel++;
=======
>>>>>>> upstream/main
            Victorytxt();
        }
        else
        {
            Defeattxt();
        }
        victoryManager = victory;
    }

    // Decrease Fire Counter
    public void KillFire()
    {
        activeFire--;
        Debug.Log("Active Fires: " + activeFire);
        if (activeFire <= 0) //if i don't have any fire// it cannot be in the update otherwise this is going to happen forever
        {
            FinishingLevel(true);
        }
    }
<<<<<<< HEAD
    
=======

    // Start is called before the first frame update
>>>>>>> upstream/main
    void Start()
    {
        bestScore = PlayerPrefs.GetInt ("Score", 0);//read the save data from the registry
        LoadLevel(); //i need to change this to load main menu than if i press the button play i can call loadlevel
    }
    
    void Update()
    {
        fires.text = activeFire.ToString();

        //all values debug.log
        /* Debug.Log("Damage: " + damage);
         Debug.Log("Total of Points: " + playerPoints.ToString("000"));
         Debug.Log("Penalty Points: " + penaltyPoints);
         Debug.Log("Time Points: " + timePoints + "Time Passed: " + timePassed + "average time: " + averageTime+ "score for time: "+ scoreForTime);
         Debug.Log("Bonus Points: " + bonusPoints);
         Debug.Log("Ending Level Points: " + endingLevelPoints);*/

        timePassed = Time.time - startingTime;
        //Debug.Log("Time Passed: "+timePassed);

        string minutes;
        minutes = ((int) timePassed / 60).ToString();

        string seconds;
        seconds = (timePassed % 60).ToString("00");

        txtTimer.text = minutes + ":" + seconds;
        
        //damage update
        if (damage <= 100)//if is bigger than 100 the damage will be 100 otherwise it will finilize the level forever, loop
        {
            damage += (damageSpeed * activeFire) * Time.deltaTime;
        }
        else
        {
            damage = 100;
        }
        txtDamage.text = damage.ToString("00") + "%";

        
        //water
        txtWater.text = waterPercentage.ToString("00") + "%";
        
        if (damage > 100)
        { 
            FinishingLevel(false);
            //enabled = false;
        }
    }
}


