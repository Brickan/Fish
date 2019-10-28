using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    private GameObject startingScreen, credits;
    // Start is called before the first frame update
    void Start()
    {
        credits = GameObject.FindGameObjectWithTag("Credits");
        startingScreen = GameObject.FindGameObjectWithTag("Starting");

        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Credits()
    {
        credits.SetActive(true);
        startingScreen.SetActive(false);
        Debug.Log("Rolling credits");
    }
}
