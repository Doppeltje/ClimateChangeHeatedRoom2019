using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	public static bool playingTheGame;
	public GameObject uiElements;

	void Start ()
	{
		StopTime();
	}

	private void Update()
	{
		if (playingTheGame) uiElements.SetActive(true);
	}

	private void StopTime()
	{
		playingTheGame = false;
		//Time.timeScale = 0;
	}

	public void ResumeTime()
	{
		playingTheGame = true;
		//Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 3f);
	}

	public void CallBackground()
	{
		GameObject.Find("BlackBG").GetComponent<Animator>().Play("BlackBGAnim");
	}
}
