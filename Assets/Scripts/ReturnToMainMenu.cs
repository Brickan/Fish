using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{
    private GameObject startingScreen, credits;

    [SerializeField]
    private bool activated;
    // Start is called before the first frame update
    void Start()
    {
        credits = GameObject.FindGameObjectWithTag("Credits");
        startingScreen = GameObject.FindGameObjectWithTag("Starting");

        activated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Submit") > 0 && !activated)
        {
            activated = true;
            ReturnToMenu();
        }
        else if(Input.GetAxis("Submit") == 0)
        {
            activated = false;
        }
    }

    public void ReturnToMenu()
    {
            startingScreen.SetActive(true);
            credits.SetActive(false);        
    }
}
