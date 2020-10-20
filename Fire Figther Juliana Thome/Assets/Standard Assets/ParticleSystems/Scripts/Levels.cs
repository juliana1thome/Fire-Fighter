using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Levels : MonoBehaviour
{
    
    //calling gameManager
    private GameManager gameManager;

    private List<GameObject> notUsedFire = new List<GameObject>();//idea: i'm creating a "box" that i can pick a random fire to light up
    public float lightUpTimeRandomFire = 0; //timer to light up a random fire
    private int i;//it works like an index so i can see where i'm at my search for not light up fires
    [SerializeField] private GameObject[] firesAvailable;
    [SerializeField] private int averageTimeToFinishLevel = 20; //time asked


    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] availableFires = GameObject.FindGameObjectsWithTag("FireNeverActive");
        foreach (var fire in firesAvailable)
        {
            if (fire.CompareTag("Not_used_fire"))//i'm calling my tag to see if it's light up or not so i can randomly light up another fire
            {
                notUsedFire.Add(fire);

            }
        }
        gameManager = GameManager.instance;
        gameManager.averageTime = averageTimeToFinishLevel;//it's in here beaucause every level has a different average time,
        //the water user story will be in here too because i need to check in each level
    }

    // Update is called once per frame
    void Update()
    {
        lightUpTimeRandomFire += Time.deltaTime;//it works to give me an idea where i'm with time. For example, it gives me the amount of time that already passed

        if (notUsedFire.Count > 0)
        {
            i = Random.Range(0, notUsedFire.Count); // my idea before was using not a list so it was notUsedFire.Legth -1, i changed because i can use .Count and .Add or .Remove
            if (lightUpTimeRandomFire > averageTimeToFinishLevel)
            {
                // Light up random fire
                gameManager = GameManager.instance;
                notUsedFire = GameManager.LightUpFiresRandomly(notUsedFire, i);

                lightUpTimeRandomFire = 0;
            }
        }
        
        //adding to my points count
        
    }
}
