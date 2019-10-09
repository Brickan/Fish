﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterScript : MonoBehaviour
{
    [Tooltip("Put the game scene here.")]
    [SerializeField]
    private string gameSceneName;

    private bool inGameScene = false;

    private void Awake()
    {
        // Makes sure we don't have any doublets of the master script.
        if (FindObjectsOfType<MasterScript>().Length > 1)
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (gameSceneName == SceneManager.GetActiveScene().name && !inGameScene)
        {
            StartCoroutine(ScoreHandler.UpdateScore(gameSceneName));
            inGameScene = true;
        }
    }


    public void LoadGameScene()
        => SceneManager.LoadSceneAsync(gameSceneName);
}
