using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : MonoBehaviour
{
	public GameObject pf_blackFade;

	private void FadeToBlack()
	{
		Instantiate(pf_blackFade, GameObject.Find("Canvas").transform);
	}
}
