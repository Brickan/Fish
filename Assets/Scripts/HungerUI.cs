using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HungerUI : MonoBehaviour
{
	private float hunger;

	[SerializeField] private Sprite[] stomick;

	private Image iStomick;

    // Start is called before the first frame update
    void Start()
    {
		iStomick = GetComponent<Image>();
		hunger = HungerScript.GetHunger();
		iStomick.sprite = stomick[0];
	}

    // Update is called once per frame
    void Update()
    {
		hunger = HungerScript.GetHunger();

		if (hunger > 66)
		{
			iStomick.sprite = stomick[0];
		}

		else if (hunger > 33 && hunger <= 66)
		{
			iStomick.sprite = stomick[1];
		}

		else if (hunger < 33)
		{
			iStomick.sprite = stomick[2];
		}
    }
}
