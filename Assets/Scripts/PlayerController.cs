using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Moving.
	private float moveSpeed, baseSpeed;
	private float horizontalMove, verticalMove;
	private bool diagonal, isIdle;
	private Vector2 movement;
	private Vector2 standStill = new Vector2(0, 0);
	private Rigidbody2D rb;

	// Facing.
	private bool facingRight;

	// Watering.
	public bool isWatering;
	public Transform canPos;
	public GameObject waterPrefab;
	private Animator noWaterLeftAnim;

	// Components.
	private Animator anim;
	public LayerMask whatIsFire;
	private UI _ui;

	// Mobile controls.
	public Button button;
	public Joystick joystick;

	void Start ()
	{
		// Components.
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		noWaterLeftAnim = GameObject.Find("NoWaterLeft").GetComponent<Animator>();
		_ui = FindObjectOfType<UI>();

		// Moving.
		moveSpeed = 7;
		baseSpeed = moveSpeed;

		// Animation.
		anim.SetBool("isIdle", true);

		// Mobile.
		button.onClick.AddListener(UseWaterCan);
	}
	
	void Update ()
	{
		Facing();
		MovingSpeed();
		//if (!UI.gameOver) Movement();
		if (!UI.gameOver) HandleMobileMovement();

		//if (Input.GetKeyDown(KeyCode.LeftShift) && !isWatering) UseWateringCan();

		// Animation.
		if (UI.gameOver) Death();
	}

	private void MovingSpeed()
	{
		diagonal = horizontalMove != 0 && verticalMove != 0;
		moveSpeed = !diagonal ? 7f : 5f;
	}

	// Non-mobile movement.
	private void Movement()
	{
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			if (!isWatering)
			{
				horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
				verticalMove = Input.GetAxisRaw("Vertical") * moveSpeed;
				movement = new Vector2(horizontalMove, verticalMove);
				rb.velocity = Vector2.Lerp(rb.velocity, movement, 15 * Time.deltaTime);
				anim.SetBool("isIdle", false);
				anim.SetBool("isWalking", true);
			}
		} else
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			rb.velocity = standStill;
		}
	}

	// Ensures facing in the right direction.
	private void Facing()
	{
		if (facingRight && horizontalMove < 0 || !facingRight && horizontalMove > 0)
		{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	// Main skill: Use the water can.
	public void UseWaterCan()
	{
		Vector2 boxSize = new Vector2(.2f, .2f);
		Collider2D[] fires = Physics2D.OverlapBoxAll(canPos.position, boxSize, whatIsFire);
		for (int i = 0; i < fires.Length; i++)
		{
			if (fires.Length > 0)
			{
				if (_ui.curWater >= _ui.waterCost)
				{
					if (fires[i].tag == "Fire") fires[i].GetComponent<Fire>().DestroyFire();
				}
			}
		}

		_ui.curWater -= _ui.waterCost;
		moveSpeed = 0;
		rb.velocity = standStill;
		isWatering = true;
		anim.SetTrigger("Water");
		if (_ui.curWater >= _ui.waterCost) Instantiate(waterPrefab, GameObject.Find("watering can").transform);
		if (_ui.curWater < _ui.waterCost) noWaterLeftAnim.Play("NoWaterLeft");
	}

	private void AfterWater() // Called by animation.
	{
		moveSpeed = baseSpeed;
		isWatering = false;
	}

	private void Death()
	{
		Vector3 deadScale = transform.localScale;
		deadScale.x = 1;
		transform.localScale = deadScale;
		moveSpeed = 0;
		rb.velocity = standStill;
		anim.SetBool("isWalking", false);
		anim.SetBool("isIdle", false);
		anim.SetBool("Death", true);
		_ui.GameOver();
	}

	// MOBILE CONTROLS.
	private void HandleMobileMovement()
	{
		if (joystick.Horizontal > .2f)
		{
			horizontalMove = moveSpeed;
		} else if (joystick.Horizontal < -.2f)
		{
			horizontalMove = -moveSpeed;
		} else
		{
			horizontalMove = 0;
		}

		if (joystick.Vertical > .2f)
		{
			verticalMove = moveSpeed;
		} else if (joystick.Vertical < -.2f)
		{
			verticalMove = -moveSpeed;
		} else
		{
			verticalMove = 0;
		}

		// Animator booleans.
		if (horizontalMove != 0 || verticalMove != 0)
		{
			anim.SetBool("isIdle", false);
			anim.SetBool("isWalking", true);
		} else
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
		}

		movement = new Vector2(horizontalMove, verticalMove);
		if (!isWatering) rb.velocity = Vector2.Lerp(rb.velocity, movement, 15 * Time.deltaTime);
	}




}
