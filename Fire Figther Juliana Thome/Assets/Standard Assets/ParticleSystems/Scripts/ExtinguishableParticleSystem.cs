using System;
using UnityEngine;


namespace UnityStandardAssets.Effects
{
    public class ExtinguishableParticleSystem : MonoBehaviour
    {
        [SerializeField] private float multiplier = 1f;
        [SerializeField] private float reduceFactor = 0.8f;
        [SerializeField] private GameObject checkbox = null;
        [SerializeField] private new Light light = null;
        private ParticleSystem[] m_Systems;
        private AudioSource audioS;
        bool stop = false;

        //adding my gameManager
        private GameManager gameManager;

        private void Start()
        {
            checkbox.SetActive(false);
            m_Systems = GetComponentsInChildren<ParticleSystem>();
            audioS = GetComponent<AudioSource>();
            
            //recovering game manager instance
            gameManager = GameManager.instance;
            //creating fires
            gameManager.MoreFire();
        }


        public void Extinguish()
        {
            //if (stop) return;
            multiplier *= reduceFactor;
            audioS.volume *= reduceFactor;
            light.intensity *= reduceFactor;
            foreach (var system in m_Systems)
            {
                if (multiplier < 0.01f)
                {
                    var emission = system.emission;
                    emission.enabled = false;
                    checkbox.SetActive (true);
                    light.enabled = false;
                    audioS.enabled = false;
                    if (stop == false)
                    {
                        stop = true;
                        Debug.Log("Killing" );
                        gameManager.KillFire();
                    }
                    
                }
                else
                {
                    ParticleSystem.MainModule mainModule = system.main;
                    mainModule.startSizeMultiplier *= reduceFactor;
                    mainModule.startSpeedMultiplier *= reduceFactor;
                    system.Play();
                }
            }
        }
    }
}
