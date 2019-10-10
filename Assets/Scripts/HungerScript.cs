using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerScript : MonoBehaviour
{
    private static float stat_hunger = 0.0f;
    private static float stat_hungerMultiplier = 0.0f;

    [Tooltip("Define the maximum value of the hunger meter. (default: 100)")]
    [SerializeField]
    private float startHunger = 100.0f;

    [Tooltip("Alters the speed at which the fish gets hungrier. (default: 1)")]
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float hungerMultiplier = 1.0f;

    void Start()
    {
        // copying the inspector values to the static variables for use in static methods and coroutines.
        stat_hunger = startHunger;
        stat_hungerMultiplier = hungerMultiplier;
    }

    public void StartHunger ()
    {
        StartCoroutine(UpdateHunger());
    }

    public static float GetHunger ()
    {
        return stat_hunger;
    }

    private static IEnumerator UpdateHunger ()
    {
        float startHunger = stat_hunger;

        while (stat_hunger < 0)
        {
            stat_hunger -= Time.deltaTime * stat_hungerMultiplier;

            yield return new WaitForFixedUpdate();
        }
    }
}