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
    private float timeMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        fadeMaterial = GetComponent<MeshRenderer>().materials[materialPicker];
        startingColor = fadeMaterial.color;
        alphaValue = startingColor.a;
    }

    // Update is called once per frame
    void Update()
    {
        Decolor();
    }

    void Decolor()
    {
        alphaValue -= Time.deltaTime * timeMultiplier;

        fadeMaterial.color = new Color(startingColor.r, startingColor.g, startingColor.b, alphaValue);
    }
}
