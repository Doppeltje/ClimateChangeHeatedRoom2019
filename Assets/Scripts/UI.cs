using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

	public Slider tempSlider, waterSlider;
	public GameObject[] prefabs;
	private FireController _fc;
	private float increase = 5f;
	private float lerpSpeed = 2f;
	private float maxTemp, curTemp;
	private float maxWater;
	public float curWater;
	public float waterCost = 10;

	public static bool gameOver;
	private bool isCreated;

	// Room color.
	private Image roomColor;

	void Start()
	{
		maxTemp = 100;
		curTemp = 10;

		maxWater = 100;
		curWater = 100;

		_fc = FindObjectOfType<FireController>();
		roomColor = GameObject.Find("RoomHeatColor").GetComponent<Image>();
	}

	void Update()
	{
		UpdateTemp();
		UpdateWater();

		if (!gameOver && TimeController.playingTheGame)
		{
			if (_fc.fireCount > 0) curTemp += (increase * _fc.fireCount) * Time.deltaTime;
			if (_fc.fireCount <= 0) curTemp -= increase * Time.deltaTime;
		}

		if (curTemp >= maxTemp) gameOver = true;
	}

	private void CalculateRoomColor() // Sets opacity of color linked to temperature.
	{
		var tempColor = roomColor.color;
		tempColor.a = tempSlider.value - .2f;
		roomColor.color = tempColor;
	}

	private void UpdateTemp() // Updates room temperature slider.
	{
		tempSlider.value = Mathf.Lerp(tempSlider.value, CalculateTemp(), lerpSpeed * Time.deltaTime);
		CalculateRoomColor();
	}

	private void UpdateWater() // Updates water slider.
	{
		waterSlider.value = Mathf.Lerp(waterSlider.value, CalculateWater(), lerpSpeed * Time.deltaTime);
	}

	float CalculateTemp() // Calculates temperature for slider value.
	{
		return curTemp / maxTemp;
	}

	float CalculateWater() // Calculates water for slider value.
	{
		return curWater / maxWater;
	}

	public void GameOver() // Loads the fade out when game is over.
	{
		if (!isCreated)
		{
			Instantiate(prefabs[0], GameObject.Find("Canvas").transform);
			Instantiate(prefabs[1], GameObject.Find("Canvas").transform);
		}

		isCreated = true;
	}
}