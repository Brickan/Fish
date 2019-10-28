using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FogController : MonoBehaviour
{
    private GameObject player;
    private float startEndDistance;
    [SerializeField]
    private float fogMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.1921569f, 0.3254902f, 0.4235294f);
        player = GameObject.FindGameObjectWithTag("Player");
        startEndDistance = RenderSettings.fogEndDistance;
        fogMultiplier = (fogMultiplier == 0) ? fogMultiplier = 3 : fogMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        FogDenser();
    }

    void FogDenser()
    {
        RaycastHit hit;
        Physics.Linecast(transform.position, player.transform.position, out hit);


        RenderSettings.fogEndDistance = FogCalc(hit.distance);

        Debug.DrawLine(transform.position, player.transform.position, Color.magenta);
    }

    float FogCalc(float inValue)
    {
        float maxEndDist = startEndDistance - (inValue * fogMultiplier);

        if (maxEndDist < 1)
        {
            maxEndDist = 1;
        }

        return maxEndDist;
    }
}
