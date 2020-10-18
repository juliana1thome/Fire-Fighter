using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    /// <summary>
    /// Idea for this script: i need to use the tags to identify my fires
    /// </summary> 

    //calling gamemanager
    private GameManager gamemanager;

    private List<GameObject> notUsedFire = new List<GameObject>();//idea: i'm creating a box that i can pick a random fire to light up
    private float light_up_time = 0; //timer to light up a random fire
    private int index;
    [SerializeField] private GameObject[] firesAvalible;
    [SerializeField] private int timing = 20; //time asked


    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] availableFires = GameObject.FindGameObjectsWithTag("FireNeverActive");
        foreach (var fire in firesAvalible)
        {
            if (fire.CompareTag("Light_Up_Fire"))//i'm calling my tag to see if it's light up or not so i can randomly light up another fire
            {
                // Debug.Log("Found: " + i++);
                notUsedFire.Add(fire);
            }
        }
        // Debug.Log("Fires: " + fires.Count);
    }

    // Update is called once per frame
    void Update()
    {
        light_up_time += Time.deltaTime;//it will count each second (returns the amount of time that elapsed since the last frame completed.)

        // Only lit up a random fire if there is any to be lit up
        if (notUsedFire.Count > 0)
        {
            index = Random.Range(0, notUsedFire.Count); // pode usar leght -1 ;)
            if (light_up_time > timing)
            {
                // Light up random fire
                gamemanager = GameManager.instance;
                notUsedFire = gamemanager.light_up_time(notUsedFire, index);

                // Reset timer so we can activate another random fire in 20 seconds
                light_up_time = 0;
            }
        }
    }
    }
}
