using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	public static PlayerScript instance;

	private float speed = 8.0f;
	private float maxVelocity = 4.0f;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private GameObject[] arrows;

	private float height;

	private bool canWalk;

	[SerializeField]
	private AnimationClip clip;

	[SerializeField]
	private AudioClip shootClip;

	private bool shootOnce, shootTwice;

	private bool moveLeft, moveRight;

	private Button shootBtn;

	[SerializeField]
	private GameObject shield;

	private string arrow;

	public bool hasShield, isInvincible, singleArrow, doubleArrows, singleStickyArrow, doubleStickyArrows, shootFirstArrow, shootSecondArrow;

	void Awake () {
		if (instance == null) {
			instance = this;
		}

		float cameraHeight = Camera.main.orthographicSize;
		height = -cameraHeight - 0.8f;

		shootBtn = GameObject.FindGameObjectWithTag ("ShootButton").GetComponent<Button> ();
		shootBtn.onClick.AddListener (() => ShootTheArrow ());

		InitializePlayer ();
	}

	void Start () {
		
	}

	void Update () {
		//ShootTheArrow ();
	}

	void FixedUpdate () {
		PlayerWalkKeyboard ();
		MoveThePlayer ();
	}

	void InitializePlayer () {
		canWalk = true;

		switch (GameController.instance.selectedWeapon) {
		case 0:
			arrow = "Arrow";
			shootOnce = true;
			shootTwice = false;

			singleArrow = true;
			doubleArrows = false;
			singleStickyArrow = false;
			doubleStickyArrows = false;
			break;
		case 1:
			arrow = "Arrow";
			shootOnce = true;
			shootTwice = true;

			singleArrow = false;
			doubleArrows = true;
			singleStickyArrow = false;
			doubleStickyArrows = false;
			break;
		case 2:
			arrow = "StickyArrow";
			shootOnce = true;
			shootTwice = false;

			singleArrow = false;
			doubleArrows = false;
			singleStickyArrow = true;
			doubleStickyArrows = false;
			break;
		case 3:
			arrow = "StickyArrow";
			shootOnce = true;
			shootTwice = true;

			singleArrow = false;
			doubleArrows = false;
			singleStickyArrow = false;
			doubleStickyArrows = true;
			break;
		}

		Vector3 bottomBrick = GameObject.FindGameObjectWithTag ("BottomBrick").transform.position;
		Vector3 temp = transform.position;

		switch (gameObject.name) {
		case "Homosapien(Clone)":
			temp.y = bottomBrick.y + 1.19f;
			break;
		case "Joker(Clone)":
			temp.y = bottomBrick.y + 1.153f;
			break;
		case "Spartan(Clone)":
			temp.y = bottomBrick.y + 1.08f;
			break;
		case "Pirate(Clone)":
			temp.y = bottomBrick.y + 1.27f;
			break;
		case "Player(Clone)":
			temp.y = bottomBrick.y + 1.19f;
			break;
		case "Zombie(Clone)":
			temp.y = bottomBrick.y + 1.11f;
			break;
		}

		transform.position = temp;
	}

	public void PlayerShootOnce (bool shootOnce) {
		this.shootOnce = shootOnce;
		shootFirstArrow = false;
	}

	public void PlayerShootTwice (bool shootTwice) {
		if (doubleArrows || doubleStickyArrows) {
			this.shootTwice = shootTwice;
		}

		shootSecondArrow = false;
	}

	public void ShootTheArrow () {
		// If the left mouse button is pressed
		if (GameplayController.instance.levelInProgress) {
			if (shootOnce) {
				if (arrow == "Arrow") {
					Instantiate (arrows [0], new Vector3 (transform.position.x, height, 0), Quaternion.identity);
				} else if (arrow == "StickyArrow") {
					Instantiate (arrows [2], new Vector3 (transform.position.x, height, 0), Quaternion.identity);
				}

				StartCoroutine (PlayTheShootAnimation ());
				shootOnce = false;
				shootFirstArrow = true;

			} else if (shootTwice) {
				if (arrow == "Arrow") {
					Instantiate (arrows [1], new Vector3 (transform.position.x, height, 0), Quaternion.identity);
				} else if (arrow == "StickyArrow") {
					Instantiate (arrows [3], new Vector3 (transform.position.x, height, 0), Quaternion.identity);
				}

				StartCoroutine (PlayTheShootAnimation ());
				shootTwice = false;
				shootSecondArrow = true;
			}
		}
	}

	IEnumerator PlayTheShootAnimation () {
		canWalk = false;
		shootBtn.interactable = false;
		animator.Play ("PlayerShoot");
		AudioSource.PlayClipAtPoint (shootClip, transform.position);
		yield return new WaitForSeconds (clip.length);
		animator.SetBool ("Shoot", false);
		shootBtn.interactable = true;
		canWalk = true;
	}

	public void DestroyShield () {
		StartCoroutine (SetPlayerInvincible ());
		hasShield = false;
		shield.SetActive (false);
	}

	IEnumerator SetPlayerInvincible () {
		isInvincible = true;
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (3f));
		isInvincible = false;
	}

	public void StopMoving () {
		moveLeft = moveRight = false;
	}

	public void MoveThePlayerLeft () {
		moveLeft = true;
		moveRight = false;
	}

	public void MoveThePlayerRight () {
		moveLeft = false;
		moveRight = true;
	}

	void MoveThePlayer () {
		if (GameplayController.instance.levelInProgress) {
			if (moveLeft) {
				MoveLeft ();
			}

			if (moveRight) {
				MoveRight ();
			}
		}
	}

	void MoveRight () {
		float force = 0.0f;
		float velocity = Mathf.Abs (myRigidBody.velocity.x);

		float h = Input.GetAxis ("Horizontal");

		if (canWalk) {
				if (velocity < maxVelocity) {
					force = speed;
				}

				Vector3 scale = transform.localScale;
				scale.x = 1.0f;
				transform.localScale = scale;

				animator.SetBool ("Walk", true);
		}

		myRigidBody.AddForce (new Vector2 (force, 0));
	}

	void MoveLeft () {
		float force = 0.0f;
		float velocity = Mathf.Abs (myRigidBody.velocity.x);

		float h = Input.GetAxis ("Horizontal");

		if (canWalk) {
				if (velocity < maxVelocity) {
					force = -speed;
				}

				Vector3 scale = transform.localScale;
				scale.x = -1.0f;
				transform.localScale = scale;

				animator.SetBool ("Walk", true);
		}

		myRigidBody.AddForce (new Vector2 (force, 0));
	}

	void PlayerWalkKeyboard () {
		float force = 0.0f;
		float velocity = Mathf.Abs (myRigidBody.velocity.x);

		float h = Input.GetAxis ("Horizontal");

		if (canWalk) {
			// Moving right
			if (h > 0) {
				if (velocity < maxVelocity) {
					force = speed;
				}

				Vector3 scale = transform.localScale;
				scale.x = 1.0f;
				transform.localScale = scale;

				animator.SetBool ("Walk", true);

				// Moving left
			} else if (h < 0) {
				if (velocity < maxVelocity) {
					force = -speed;
				}

				Vector3 scale = transform.localScale;
				scale.x = -1.0f;
				transform.localScale = scale;

				animator.SetBool ("Walk", true);
		
				// Stops moving
			} else if (h == 0) {
				animator.SetBool ("Walk", false);
			}

			myRigidBody.AddForce (new Vector2 (force, 0));
		}
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.tag == "SingleArrow") {
			if (!singleArrow) {
				arrow = "Arrow";
				if (!shootFirstArrow) {
					shootOnce = true;
				}

				shootTwice = false;

				singleArrow = true;
				doubleArrows = false;
				singleStickyArrow = false;
				doubleStickyArrows = false;
			}
		}

		if (target.tag == "DoubleArrow") {
			if (!doubleArrows) {
				arrow = "Arrow";
				if (!shootFirstArrow) {
					shootOnce = true;
				}

				if (!shootSecondArrow) {
					shootTwice = true;
				}

				singleArrow = false;
				doubleArrows = true;
				singleStickyArrow = false;
				doubleStickyArrows = false;
			}
		}

		if (target.tag == "SingleStickyArrow") {
			if (!singleStickyArrow) {
				arrow = "StickyArrow";
				if (!shootFirstArrow) {
					shootOnce = true;
				}

				shootTwice = false;

				singleArrow = false;
				doubleArrows = false;
				singleStickyArrow = true;
				doubleStickyArrows = false;
			}
		}

		if (target.tag == "DoubleStickyArrow") {
			if (!doubleStickyArrows) {
				arrow = "StickyArrow";
				if (!shootFirstArrow) {
					shootOnce = true;
				}

				if (!shootSecondArrow) {
					shootTwice = true;
				}

				singleArrow = false;
				doubleArrows = false;
				singleStickyArrow = false;
				doubleStickyArrows = true;
			}
		}

		if (target.tag == "Watch") {
			GameplayController.instance.levelTime += Random.Range (10, 20);
		}

		if (target.tag == "Shield") {
			hasShield = true;
			shield.SetActive (true);
		}

		if (target.tag == "Dynamite") {
		}

	}

} // PlayerScript