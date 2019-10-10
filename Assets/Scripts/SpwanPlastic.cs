using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPlastic : MonoBehaviour
{
	[SerializeField] float time, startTime, minPosX, maxPosX, minPosZ, maxPosZ;
	[SerializeField] GameObject[] plast;

	// Start is called before the first frame update
	void Start()
	{
		startTime = ScoreHandler.GetPlasticSpawnRate();
		time = startTime;
	}

	// Update is called once per frame
	void Update()
	{
		time = time - Time.deltaTime;

		if (time < 0)
		{
			startTime = ScoreHandler.GetPlasticSpawnRate();
			time = startTime;
			Spawn();
		}
	}

	private void Spawn()
	{
		int randPlast = Random.Range(0, plast.Length);
		float randPosX = Random.Range(minPosX * -1, maxPosX);
		float randPosZ = Random.Range(minPosZ * -1, maxPosZ);

		Instantiate(plast[randPlast], new Vector3(randPosX, 10, randPosZ), Quaternion.identity);
	}
}
