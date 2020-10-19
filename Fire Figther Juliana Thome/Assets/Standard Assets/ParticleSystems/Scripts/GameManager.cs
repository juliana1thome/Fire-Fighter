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
    
    //fire
    private int fire = 0;// My quantity of fires will be 0 in the beginning


    /*private List<GameObject> notUsedFire = new List<GameObject>();//idea: i'm creating a box that i can pick a random fire to light up
     [SerializeField] private GameObject[] firesAvailable;
     [SerializeField] private int timing = 20; // time asked */

    //for user story 2
    [SerializeField] private int preventionScore;
    [FormerlySerializedAs("Fires")] [SerializeField] private Text fires;
    [FormerlySerializedAs("txt_timer")] [SerializeField] private Text txtTimer;// i need txt in the beginning otherwise it does not work
    [FormerlySerializedAs("Points")] [SerializeField] private Text points;
    [FormerlySerializedAs("Damage")] [SerializeField] private Text damage;
    private float timePassed = 0;
    private int totalOfFires = 0; 
    private float startingTime = 0; // Start time
    private int playerPoint = 0;
    private bool m_Victory;
    [SerializeField] private GameObject victoryScreen;



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

        fire = 0; // to be sure if the counter panel is empty

        currentBoard = Instantiate(level[curLevel]);
        Time.timeScale = 1;//volta o tempo para voltar ao normal
        victoryScreen.SetActive(false);
    }

    public void MoreFire()
    {
        fire++; //it will add more fires
        totalOfFires++;
    }

    private void AddPoints()
    {
        playerPoint += ((14 - totalOfFires) * preventionScore);//teachers recommendation for this user story
        Debug.Log("Points: " + playerPoint);//i need this so i can see my points as a player
        points.text = "Points: " + playerPoint.ToString();
        victoryScreen.SetActive(true);
        //GetComponent<Image>().gameObject.SetActive(true); first attempt
    }

    public static List<GameObject> LightUpFiresRandomly(List<GameObject> notUsedFire, int index)
    {
        //i'm lightning up a fire so i can remove it from my list
        notUsedFire[index].SetActive(true);
        // i'm removing it from my list
        notUsedFire.Remove(notUsedFire[index]);

        return notUsedFire;
    }


    //checking box
    private void Initiate()
    {
        GameObject[] checkbox = GameObject.FindGameObjectsWithTag("Checkbox"); //it will find my checkbox in unity

        //it sets all the check boxes as disabled  
        foreach (var check in checkbox)
        {
            check.SetActive(false);
        }
    }

    public void FinishingLevel(bool victory)
    {
        m_Victory = victory;
        AddPoints();
        Time.timeScale = 0;//pausing
    }

    //kill fire, it needs to be public like morefire and lessfire
    public void KillFire()
    {
        Debug.Log("Fires actives" + fire);
        fire--;
        
        if (fire <= 0)//if i don't have any fire
        {
            FinishingLevel(true);
        }
    }

    //death
    public void Death()
    {
        Debug.Log("Dead...");
    }

    public void BuildingDamage()
    {

        //damage += (damageSpeed * activeFire) * Time.deltaTime

    }

    // Start is called before the first frame update
    void Start()
    {

        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        fires.text = fire.ToString();// i had to remove 1 because i could not find the mistake that gives one extra fire

        timePassed = Time.time - startingTime;

        string minutes = ((int)timePassed / 60).ToString();

        string seconds = (timePassed % 60).ToString("00");

        txtTimer.text = minutes + ":" + seconds;

        
    }
}
