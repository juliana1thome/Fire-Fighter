using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    //levels manager
    private int curLevel = 0; // this is for my current level, so i can "find myself"
    [SerializeField] private GameObject[] level = null; //list of all levels
    private GameObject currentBoard; //this is the current level, you have it saved and them you delet it
    
    //activeFire
    private int activeFire = 0;// My quantity of fires will be 0 in the beginning


    /*private List<GameObject> notUsedFire = new List<GameObject>();//idea: i'm creating a box that i can pick a random fire to light up
     [SerializeField] private GameObject[] firesAvailable;
     [SerializeField] private int timing = 20; // time asked */

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
    
    
    //Variables for point system
    private float totalPoints;
    public int averageTime;
    private float bonusPoints;
    private float timePoints;
    private float endingLevelPoints;
    [SerializeField] private Text txtVictory;
    [SerializeField] private Text txtTotalPoints;
    [SerializeField] private Text txtPointsLevelPassed;
    [SerializeField] private Text txtbonusPoints;
    [SerializeField] private Text txttimePoints;
    [SerializeField] private Text txtDamage;
    [SerializeField] private Text txtPenaltyPoints;
    
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
        Time.timeScale = 1;//volta o tempo para voltar ao normal
        endLevelScreen.SetActive(false);
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

    public void FinishingLevel()
    {
        
        AddPoints();
        endLevelScreen.SetActive(true);
        if (victory == true)
        {
            txtVictory.text = "Victory!";
            txtPointsLevelPassed.text = "Level passed: + " + endingLevelPoints;
            //i do this in here because i want this to happen only i win
            txttimePoints.text = "Points for time: " + timePoints;
            txtbonusPoints.text = "Bonus Points: " + bonusPoints;
            txtTotalPoints.text = "Total: " + playerPoints;
            txtPenaltyPoints.text = "Penalty: - " + penaltyPoints;
        }
        
        else
        {
            txtVictory.text = "Defeat!";
            txtPointsLevelPassed.text = "Level passed: + 0";
        }
        
        //pausing
        Time.timeScale = 0;

    }

    //kill fire, it needs to be public like morefire and lessfire
    public void KillFire()
    {
        activeFire--;
        Debug.Log("Active Fires: " + activeFire);
        if (activeFire <= 0 && timePassed>= 2)//if i don't have any fire// it cannot be in the update otherwise this is going to happen forever
        {
            victory = true;
            FinishingLevel();// i need to resolve the expense use of memory
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

        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
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
