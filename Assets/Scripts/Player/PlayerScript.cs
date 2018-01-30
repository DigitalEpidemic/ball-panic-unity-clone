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

	void Awake () {
		if (instance == null) {
			instance = this;
		}

		float cameraHeight = Camera.main.orthographicSize;
		height = -cameraHeight - 0.8f;
		canWalk = true;

		shootOnce = true;
		shootTwice = true;

		shootBtn = GameObject.FindGameObjectWithTag ("ShootButton").GetComponent<Button> ();
		shootBtn.onClick.AddListener (() => ShootTheArrow ());
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

	public void PlayerShootOnce (bool shootOnce) {
		this.shootOnce = shootOnce;
	}

	public void PlayerShootTwice (bool shootTwice) {
		this.shootTwice = shootTwice;
	}

	public void ShootTheArrow () {
		// If the left mouse button is pressed
		if (GameplayController.instance.levelInProgress) {
			if (shootOnce) {
				shootOnce = false;
				StartCoroutine (PlayTheShootAnimation ());
				Instantiate (arrows [0], new Vector3 (transform.position.x, height, 0), Quaternion.identity);
			} else if (shootTwice) {
				shootTwice = false;
				StartCoroutine (PlayTheShootAnimation ());
				Instantiate (arrows [1], new Vector3 (transform.position.x, height, 0), Quaternion.identity);
			}
		}
	}

	IEnumerator PlayTheShootAnimation () {
		canWalk = false;
		animator.Play ("PlayerShoot");
		AudioSource.PlayClipAtPoint (shootClip, transform.position);
		yield return new WaitForSeconds (clip.length);
		animator.SetBool ("Shoot", false);
		canWalk = true;
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

} // PlayerScript