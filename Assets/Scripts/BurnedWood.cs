using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnedWood : MonoBehaviour
{
	private GameObject theRoom;

	void Start ()
	{
		theRoom = GameObject.Find("The Room");
		transform.parent = theRoom.transform;
	}
}
