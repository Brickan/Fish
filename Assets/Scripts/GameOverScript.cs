using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private Image UI;

    void Start()
    {
        UI.gameObject.SetActive(false);

        StartCoroutine(GameOver());
    }


    private IEnumerator GameOver ()
    {
        foreach (Sprite spr in sprites)
        {
            yield return new WaitForSeconds(0.5f);

            UI.sprite = spr;
            UI.gameObject.SetActive(true);

            yield return new WaitForSeconds(5);
            UI.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(0);
    }
}
