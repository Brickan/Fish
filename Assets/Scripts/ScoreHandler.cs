using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    private static float score = 0.0f;

    [Tooltip("The rate at which the difficulty will incrementally increase.")]
    [Range(1.01f, 1.5f)]
    [SerializeField]
    private float difficulty = 1.1f;
    private static float sDifficulty = 0.0f;

    [Tooltip("Decide the start value of the 'C' constant in the exponentially increasing spawn rate of plastic.")]
    [SerializeField]
    private float startValue = 1.0f;
    private static float sStartValue = 0.0f;

    private static float plasticSpawnRate = 0.0f;

    private static bool inGameScene = false;

    void Start()
    {
        sDifficulty = difficulty;
        sStartValue = startValue;
    }


    /// <summary>
    /// Returns the current score.
    /// </summary>
    /// <returns></returns>
    public static float GetScore ()
    {
        return score;
    }


    /// <summary>
    /// Retuns the current plastic spawn rate.
    /// </summary>
    /// <returns></returns>
    public static float GetPlasticSpawnRate ()
    {
        // f(x) = C * a^x
        // f(x) = plasticSpawnRate
        // C    = start value (1 by default)
        // a    = difficulty
        // x    = score

        plasticSpawnRate = sStartValue * Mathf.Pow(sDifficulty, score);
        return plasticSpawnRate;
    }


    /// <summary>
    /// Runs the score thingy while in the 'game scene'.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public static IEnumerator UpdateScore (int a)
    {
        while (SceneManager.GetActiveScene().buildIndex == a)
        {
            score += Time.deltaTime;
            Debug.Log("SCORE: " + score + " DIFFICULTY: " + GetPlasticSpawnRate());

            yield return new WaitForFixedUpdate();
        }

        score = 0.0f;
    }
}
