using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorallFade : MonoBehaviour
{
    private Material fadeMaterial;
    private Color startingColor;
    private float alphaValue;

    [SerializeField]
    private int materialPicker;
    [SerializeField]
    private float timeToChange;
    [SerializeField]
    private bool minutesFade;


    // Start is called before the first frame update
    void Start()
    {
        fadeMaterial = GetComponent<MeshRenderer>().materials[materialPicker];
        startingColor = fadeMaterial.color;
        alphaValue = startingColor.a;

        timeToChange = (timeToChange == 0) ? timeToChange = 20 : timeToChange;

        TimeMultiplierMath();
    }

    // Update is called once per frame
    void Update()
    {
        Decolor();
    }

    void Decolor()
    {
        TimeCalc();


        fadeMaterial.color = new Color(startingColor.r, startingColor.g, startingColor.b, alphaValue);
    }

    void TimeMultiplierMath()
    {
        if (minutesFade)
        {
            timeToChange = (1f / 60f) / timeToChange;
        }
        else
        {
            timeToChange = 1f / timeToChange;
        }
    }

    void TimeCalc()
    {
        alphaValue -= Time.deltaTime * timeToChange;

        if(alphaValue < 0)
        {
            alphaValue = 0;
        }
    }
}
