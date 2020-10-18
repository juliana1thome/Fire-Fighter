using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //levels manager
    private int curLevel = 0; // this is for my current level, so i can "find myself"
    [SerializeField] private GameObject[] level = null; //list of all levels
    private GameObject currentBoard; //this is the current level, you have it saved and them you delet it
    
    //fire
    private int fire = 0;// My quantaty of fires will be 0 in the beginning


    //for user story 2
    private List<GameObject> notUsedFire = new List<GameObject>();//idea: i'm creating a box that i can pick a random fire to light up
    [SerializeField] private GameObject[] firesAvalible;
    [SerializeField] private int timing = 20; // time asked


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

        if (currentBoard)// If any  level is loaded it will kill it
        {
            Destroy(currentBoard);
        }

        fire = 0; // to be sure if the counter panel is empty

        currentBoard = Instantiate(level[curLevel]); // Copy the level at position 
    }

    public void MoreFire()
    {
        fire++;//it will add more fires
    }

    public void LessFire()
    {
        fire--;//it will reduce my fires
    }


    //checking box
    private void Initiate()
    {
        GameObject[] checkbox = GameObject.FindGameObjectsWithTag("Checkbox"); //it will find my checkbox in unity

        //it sets all the check boxes as disabled  
        for (int i = 0; i < checkbox.Length; i++)
        {
            checkbox[i].SetActive(false);
        }
    }


    //kill fire, it needs to be public like morefire and lessfire
    public void KillFire()
    {
        //reducing the quantaty of fires when you kill it
        LessFire();

        if (fire <= 0)//if i don't have any fire
        {
            if (curLevel < level.Length - 1)//if i have one more level play it
            {
                curLevel++;
            }

            //otherwise display this, and repeat
            else Debug.Log("Game ended, a repetion will occurrer"); //trigger a victory end screen to display results
            Initiate();
            LoadLevel();
        }
    }

    //death
    public void Death()
    {
        Debug.Log("Death");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
