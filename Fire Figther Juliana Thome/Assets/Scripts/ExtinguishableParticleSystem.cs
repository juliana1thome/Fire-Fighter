using System;
using UnityEngine;


namespace UnityStandardAssets.Effects
{
    public class ExtinguishableParticleSystem : MonoBehaviour
    {
        [SerializeField] private float multiplier = 2f;
        [SerializeField] private float reduceFactor = 0.80f;// Already have i'm just going to use it
        [SerializeField] private GameObject checkbox = null;
        [SerializeField] private new Light light = null;
        private ParticleSystem[] m_Systems;
        private AudioSource audioS;
        bool stop = false;
        
        // User story 6 
        private float timerForFires;
        [SerializeField] private float increaseFactor = 1.2f;// I added the increaseFactor to update my fire size all the time if i have too, if i remove 2 i increase 2
        
        // Adding my gameManager
        private GameManager gameManager;

        private void Start()
        {
            checkbox.SetActive(false);
            stop = false;
            m_Systems = GetComponentsInChildren<ParticleSystem>();
            audioS = GetComponent<AudioSource>();
            this.gameObject.tag = "Light_Up_Fire";
            // Recovering game manager instance
            gameManager = GameManager.instance;
            // Creating fires
            gameManager.MoreFire();

        }

        private void FireSizeUpdate(float factor)
        {
            multiplier *= factor;
            foreach (ParticleSystem system in m_Systems)// For my particle do the same thing as my values
            {
                ParticleSystem.MainModule mainModule = system.main;
                mainModule.startSizeMultiplier *= factor;
                mainModule.startSpeedMultiplier *= factor;
                system.Play();
            }
        }

       public void Extinguish()
        {
            // Ok so my fire was extinguished, so now i need to set the timer = 0
            FireSizeUpdate(reduceFactor);
            light.intensity *= reduceFactor;
            timerForFires = 0;
            
            foreach (var system in m_Systems) // To find all the particle systems i have
            {
                if (multiplier <= 0.01f) // i change because i was verifying all the time many things but i was just verifying and i was having a delay, so i was never able to do the otherthings
                {
                
                    var emission = system.emission;
                    emission.enabled = false;
                    checkbox.SetActive(true);
                    light.enabled = false;
                    audioS.enabled = false;
                    if (stop == false)
                    {
                        stop = true;
                        Debug.Log("Killing");
                        gameManager.KillFire();
                    }
                }
            }
        }

       void Update()
        {
            timerForFires += Time.deltaTime;
            if (timerForFires > 1 && multiplier < 2)// If it took too long to turn off the fire the update will call the function firesizeupdate and it will say to increase the fire by 1
            {
                // Debug.Log("Multiplier depois do if update" + multiplier);

                FireSizeUpdate(increaseFactor);

                if (multiplier >= 2.0f)
                {
                   // Debug.Log("Multiplier depois depois do if update" + multiplier);

                    timerForFires = 0;
                }
            }
        }
    }
}
