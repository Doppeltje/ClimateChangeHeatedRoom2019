using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	private Animator anim;
	public GameObject[] burnedWood;
	private GameObject burningWood;
	private FireController _fc;
	public CircleCollider2D coll;
	private bool fireOut;

	private void Start()
	{
		anim = GetComponent<Animator>();
		_fc = FindObjectOfType<FireController>();
	}

	public void DestroyFire()
	{
		Instantiate(burnedWood[Random.Range(0, burnedWood.Length)], transform);
		anim.Play("FadeOut");
		_fc.fireCount--;
		if (gameObject != null) Destroy(gameObject, 2.5f);
	}
}
