using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	public GameObject[] spawnPoints;
	public GameObject firePrefab;
	public int fireCount = 0;
	private float spawnCounter;
	private float spawnSpeed = 4;

	private void Update()
	{
		spawnCounter += Time.deltaTime;

		if (spawnCounter >= spawnSpeed) SpawnFire();
	}

	private void SpawnFire()
	{
		fireCount++;
		spawnCounter = 0;
		Instantiate(firePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform);
	}

	
}
