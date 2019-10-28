using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsButton : MonoBehaviour
{
    private GameObject startingScreen, instructions;
    // Start is called before the first frame update
    void Start()
    {
        instructions = GameObject.FindGameObjectWithTag("Instructions");
        startingScreen = GameObject.FindGameObjectWithTag("Starting");

        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Instructions()
    {
        instructions.SetActive(true);
        startingScreen.SetActive(false);
        Debug.Log("Rolling credits");
    }
}
