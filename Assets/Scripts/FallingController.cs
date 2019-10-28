using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
	[SerializeField] private float rmin, rmax, zmin, zmax, time, startTime, timeR;

	private float rand, rand2, checkValue;

	bool bottom;

	private Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		if (rmin == 0)
		{
			rmin = -5;
		}

		if (rmax == 0)
		{
			rmax = 5;
		}
	}

	// Update is called once per frame
	void Update()
	{
		ClampMove();
		CountDown();
	}

	void ClampMove()
	{

			rand = Random.Range(rmin, rmax);
			rand2 = Random.Range(zmin, zmax);
	}

	void CountDown()
	{
		time = time - Time.deltaTime;

		if (time <= 0)
		{
			Move();
			time = startTime;
		}

		timeR = timeR - Time.deltaTime;

		if (timeR < 0)
		{
			Destroy(rb);
			Destroy(this);
		}

	}

	void Move()
	{
		rb.velocity = new Vector3(rand, 0, rand2);
	}

	private void OnCollisionEnter(Collision collision)
	{
		//bottom = true;
	}
}