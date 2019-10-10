using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuControllerChecker : MonoBehaviour
{
    private int buttonInt, currentButton;
    private bool UseCursor, moved;

    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
       
        buttonInt = GameObject.FindGameObjectWithTag("Menu").GetComponentsInChildren<Button>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        ControllerCheck();

        if(!UseCursor)
        {
            ChangeButton();
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void ChangeButton()
    {
        if(Input.GetAxis("Vertical") > 0 && !moved)
        {
            currentButton -= 1;
            moved = true;
        }
        else if(Input.GetAxis("Vertical") < 0 && !moved)
        {
            currentButton += 1;
            moved = true;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            moved = false;
        }

        if(currentButton < 0)
        {
            currentButton = buttonInt - 1;
        }
        else if(currentButton == buttonInt)
        {
            currentButton = 0;
        }
        else
        {

        }

        Button thisButton = GameObject.FindGameObjectWithTag("Menu").GetComponentsInChildren<Button>()[currentButton];
        thisButton.Select();
        
        if(Input.GetAxis("Submit") > 0)
        {
            thisButton.onClick.Invoke();
        }

    }

    void SelectButton()
    {

    }

    void ControllerCheck()
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            if (Input.GetJoystickNames()[0] == "Wireless Controller")
            {
                UseCursor = false;
            }
            else
            {
                UseCursor = true;
            }
        }
        else
        {
            UseCursor = true;
        }
        Cursor.visible = UseCursor ? true : false;
        Cursor.lockState = UseCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
