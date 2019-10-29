using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{
    private GameObject startingScreen, credits, instructions;

    [SerializeField]
    private bool activated;
    // Start is called before the first frame update
    void Awake()
    {
        credits = GameObject.FindGameObjectWithTag("Credits");
        startingScreen = GameObject.FindGameObjectWithTag("Starting");
        instructions = GameObject.FindGameObjectWithTag("Instructions");

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

    private void ReturnToMenu()
    {
            credits.SetActive(false);
            startingScreen.SetActive(true);
            instructions.SetActive(false);
    }
}
