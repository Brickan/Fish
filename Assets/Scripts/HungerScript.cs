using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private int deathSceneIndex = 0;
    private static int deathIndex;

    void Start()
    {
        // copying the inspector values to the static variables for use in static methods and coroutines.
        stat_hunger = startHunger;
        stat_hungerMultiplier = hungerMultiplier;

        deathIndex = deathSceneIndex;

        StartHunger();
    }

    /// <summary>
    /// Starts the hunger meter.
    /// </summary>
    private void StartHunger ()
    {
        StartCoroutine(UpdateHunger());
    }

    /// <summary>
    /// Returns the current hunger level as a float.
    /// </summary>
    /// <returns></returns>
    public static float GetHunger ()
    {
        return stat_hunger;
    }

    /// <summary>
    /// Increases the hunger meter by a referenced amount, meaning it feeds the fish.
    /// </summary>
    /// <param name="foodLevel"></param>
    public static void IncreaseHungerMeter (float foodLevel)
    {
        stat_hunger += foodLevel;
    }

    private static IEnumerator UpdateHunger ()
    {
        while (stat_hunger > 0)
        {
            stat_hunger -= Time.deltaTime * stat_hungerMultiplier;

            yield return new WaitForFixedUpdate();
        }

        DeadFish();
    }

    /// <summary>
    /// Only to be used if we wanna brutaly murder the fish by way if this script. (Unimplemented)
    /// </summary>
    private static void DeadFish ()
    {
        SceneManager.LoadSceneAsync(deathIndex);
    }
}