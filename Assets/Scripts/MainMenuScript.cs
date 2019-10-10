using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadIndex (int a)
    {
        FindObjectOfType<MasterScript>().sceneIndex = a;
        SceneManager.LoadSceneAsync(a);
    }
}
