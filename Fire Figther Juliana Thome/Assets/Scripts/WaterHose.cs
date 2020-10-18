using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterHose : MonoBehaviour
{
    private AudioSource audioS;
    private bool click;
    [SerializeField] private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = 0;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        click = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        if (click && audioS.volume < 1f)
        {
            audioS.volume = Mathf.Lerp (audioS.volume, 1, Time.deltaTime * speed);
        }else if (!click && audioS.volume > 0f)
        {
            audioS.volume = Mathf.Lerp (audioS.volume, 0, Time.deltaTime * speed);
        }
        
    }
}
