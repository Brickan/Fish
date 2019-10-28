using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    private float imageInterval = 5;

    [SerializeField]
    private List<Sprite> img;

    [SerializeField]
    private GameObject UI;

    [SerializeField]
    private Image UI_img;

    void Start()
    {
        UI_img = UI.GetComponent<Image>();
        UI.SetActive(false);
    }


    private IEnumerator GameOver ()
    {
        foreach (Sprite image in img)
        {
            yield return new WaitForSeconds(.5f);

            if (UI.activeInHierarchy == false)
                UI.SetActive(true);

            UI_img.sprite = image;

            yield return new WaitForSeconds(imageInterval);
            UI.SetActive(false);
        }
    }
}
