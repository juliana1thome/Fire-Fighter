using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorLock : MonoBehaviour
{

    private bool lockState = true;
    bool cancelled = false;
    bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        cancelled = context.performed;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        clicked = context.performed;
    } 

    // Update is called once per frame
    void Update()
    {
        if (cancelled && lockState)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            lockState = false;
            cancelled = false;
        }
        else 
        if (clicked && !lockState)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            lockState = true;
            clicked = false;
        }
      
    }
}
