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
    /// user story 4 --> Game Over Exit Button --> Credit: Marc-André Larouche
    /// </summary>

    //levels manager
    private int curLevel = 0; // this is for my current level, so i can "find myself"
    [SerializeField] private GameObject[] level; //list of all levels
    private GameObject currentBoard; //this is the current level, you have it saved and them you delet it
    
    //activeFire
    private int activeFire = 0;// My quantity of fires will be 0 in the beginning

    //for user story 2
    [SerializeField] private int preventionScore;
    [SerializeField] private Text fires;
    [SerializeField] private Text txtTimer;// i need txt in the beginning otherwise it does not work
    private float timePassed = 0;
    private int firesNeverActive = 14; 
    private float startingTime = 0; // Start time
    private float playerPoints = 0;
    [SerializeField] private GameObject endLevelScreen;//i'm going to use for user story 3

    //user story 3
    [SerializeField] private int pointsPerLevel = 200;//if i add levels i can increment it depending of the level
    [SerializeField] private int scoreForTime = 50;
    private bool victory;
    
    //user story 4 --> Damage
    private float damage = 0;
    private string penaltyPoints;
    [SerializeField] private float damageSpeed = 0.5f;
    
    //user story 4 --> Game Over screen
    [SerializeField] private Button exitButton; 
   //public GameObject restartButton;
   [SerializeField] private Button restartButton;
    public string sceneToReload = "FireFighter";
    
    //Variables for point system
    private float totalPoints;
    public int averageTime;
    private float bonusPoints;
    private float timePoints;
    private float endingLevelPoints;
    
    //Txt
    [SerializeField] private Text txtVictory;
    [SerializeField] private Text txtTotalPoints;
    [SerializeField] private Text txtPointsLevelPassed;
    [SerializeField] private Text txtbonusPoints;
    [SerializeField] private Text txttimePoints;
    [SerializeField] private Text txtDamage;
    [SerializeField] private Text txtPenaltyPoints;
    [SerializeField] private Text txtExitButton;
    [SerializeField] private Text txtRestartButton;
    
    //Singleton
    public static GameManager instance = null; // referencing my singleton

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

    //level loading

    private void LoadLevel()
    {
        Invoke(nameof(Initiate), 0);//initialize level to make sure everything, see more about this

        if (currentBoard)//if i have a level loaded it will destroy it 
        {
            Destroy(currentBoard);
        }

        activeFire = 0; // to be sure if the counter panel is empty

        currentBoard = Instantiate(level[curLevel]);
        Time.timeScale = 0;
        endLevelScreen.SetActive(false);
    }

    //exit function

    public void Exit()
             
    {
        Application.Quit();
    }

    //RESTART
    public void Restart()
    {
        LoadLevel();
        //SceneManager.LoadScene(sceneToReload);
    }


    public void MoreFire()
    {
        activeFire++; //it will add more fires
        firesNeverActive--;
    }

    private void AddPoints()
    {
        if (victory == true)
        {
            endingLevelPoints= pointsPerLevel;
            playerPoints += endingLevelPoints;
            Debug.Log("Ending Level Points: " + endingLevelPoints);
        }
        else
        {
            endingLevelPoints= 0;
            playerPoints += endingLevelPoints;
        }
        
        //adding all the points
        playerPoints += pointsPerLevel;
        Debug.Log("Points: " + pointsPerLevel);//i need this so i can see my points as a player

        // time points
        timePoints = ((averageTime / timePassed) * scoreForTime);
        playerPoints += timePoints;
        Debug.Log("Time Points: " + timePoints + "Time Passed: " + timePassed + "average time: " + averageTime+ "score for time: "+ scoreForTime);
       
        //bonus points
        bonusPoints = ((14 - firesNeverActive) * preventionScore);//teachers recommendation for the user story 2
        playerPoints += bonusPoints;
        Debug.Log("Bonus Points: " + bonusPoints);

        //penalty points
        // Add penalty points
        playerPoints -= 10 * damage;
        penaltyPoints = (10 * damage).ToString("000");
        Debug.Log("Penalty Points: " + penaltyPoints);
        
        //teacher's requirement, this is for my playerPoints never be less than 0
        if (playerPoints < 0)
        {
            playerPoints = 0;
        }
        
        //just to see if it's ok
        Debug.Log("Total of Points: " + playerPoints.ToString("000"));

    }
    
    public static List<GameObject> LightUpFiresRandomly(List<GameObject> notUsedFire, int i)
    {
        //i'm lightning up a fire so i can remove it from my list
        notUsedFire[i].SetActive(true);
        // i'm removing it from my list
        notUsedFire.Remove(notUsedFire[i]);
        
        return notUsedFire;
    }
    
    //checking box
    private void Initiate()
    {
        GameObject[] checkbox = GameObject.FindGameObjectsWithTag("Checkbox"); //it will find my checkbox in unity
        damage = 0; //if i don't do this i will play another game with trash points
        Debug.Log("Damage: " + damage);
        //it sets all the check boxes as disabled  
        foreach (var check in checkbox)
        {
            check.SetActive(false);
        }
        
        txtDamage.text = damage.ToString("000");
    }

    public void FinishingLevel()//my gameover is in here too
    {
        
        AddPoints();
        endLevelScreen.SetActive(true);
        
        if (victory == true)
        {
            txtVictory.text = "Congratulations, You Just Won The Level!!!";
            txtPointsLevelPassed.text = "Level passed: + " + endingLevelPoints;
            //i do this in here because i want this to happen only i win
            txttimePoints.text = "Points for time: " + timePoints;
            txtbonusPoints.text = "Bonus Points: " + bonusPoints;
            txtTotalPoints.text = "Total: " + playerPoints;
            txtPenaltyPoints.text = "Penalty: - " + penaltyPoints;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            exitButton.onClick.AddListener(delegate {Exit();});
            restartButton.onClick.AddListener(delegate { Restart(); });
            //to next level button
            /*if (curLevel < level.Length -1)
            {
                curLevel++;
            }*/
            //restartButton.onClick.AddListener(delegate { LoadLevel(); });//change this to next level
        }
        
        else
        {
            /*i want to allow the button to show up and i want this to happen every time you have the endlevelscreen,
             so if you won you can restart the level to get more points, or you can go to the next level
            */
            //problem: i cannot see my cursor, i cannot select the buttons
            txtVictory.text = "Defeated! Try again!";
            txtPointsLevelPassed.text = "Level passed:  0";
            txtExitButton.text = "Exit";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            exitButton.onClick.AddListener(delegate {Exit();});
            //restartButton.onClick.AddListener(delegate { LoadLevel(); });
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        //pausing
        Time.timeScale = 0;

    }

    //kill fire, it needs to be public like morefire and lessfire
    public void KillFire()
    {
        activeFire--;
        Debug.Log("Active Fires: " + activeFire);
        if (activeFire <= 0 && timePassed>= 2 && damage >= 100)//if i don't have any fire// it cannot be in the update otherwise this is going to happen forever
        {
            victory = true;
            FinishingLevel();
        }
    }

    //death
    public void Death()
    {
        Debug.Log("Dead...");
    }

    /*public void Damage()
    {

        //damage += (damageSpeed * activeFire) * Time.deltaTime

    }*/

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();//i need to change this to load main menu than if i press the button play i can call loadlevel
    }

    // Update is called once per frame
    void Update()
    {
        if (damage >= 100)
        {
            FinishingLevel();
        }
        
        fires.text = activeFire.ToString();// i had to remove 1 because i could not find the mistake that gives one extra fire

        timePassed = Time.time - startingTime;
       // Debug.Log("Time Passed: "+timePassed);

        string minutes; 
        minutes= ((int)timePassed / 60).ToString();

        string seconds; 
        seconds = (timePassed % 60).ToString("00");

        txtTimer.text = minutes + ":" + seconds;
        
        //damage update since i will make damage to the building all the time because i need to count the time
        damage += (damageSpeed * activeFire) * Time.deltaTime;
        txtDamage.text = damage.ToString("000");
        
    }
}
