using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPlastic : MonoBehaviour
{
	[SerializeField] float time, startTime, minPosX, maxPosX, minPosZ, maxPosZ, minPosY, maxPosY;
	[SerializeField] GameObject[] plast;

	[SerializeField] bool isPlastic;

	// Start is called before the first frame update
	void Start()
	{
		time = startTime;
	}

	// Update is called once per frame
	void Update()
	{

		if (isPlastic == true)
			time = time - Time.deltaTime * ScoreHandler.GetPlasticSpawnRate();

		else
			time = time + Time.deltaTime * ScoreHandler.GetPlasticSpawnRate() * -1;

		if (time < 0)
		{
			time = startTime;
			Spawn();
		}
	}

	private void Spawn()
	{
		int randPlast = Random.Range(0, plast.Length);
		float randPosX = Random.Range(minPosX * -1, maxPosX);
		float randPosZ = Random.Range(minPosZ * -1, maxPosZ);
		float randposY = Random.Range(minPosY * -1, maxPosY);

		Instantiate(plast[randPlast], new Vector3(randPosX, randposY, randPosZ), Quaternion.identity);
	}
}