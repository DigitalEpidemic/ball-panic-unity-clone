using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Awake () {
		if (instance == null) {
			instance = this;
		}

		float cameraHeight = Camera.main.orthographicSize;
		height = -cameraHeight - 0.8f;
		canWalk = true;

		shootOnce = true;
		shootTwice = true;
	}

	void Start () {
		
	}

	void Update () {
		ShootTheArrow ();
	}

	void FixedUpdate () {
		PlayerWalkKeyboard ();
	}

	public void PlayerShootOnce (bool shootOnce) {
		this.shootOnce = shootOnce;
	}

	public void PlayerShootTwice (bool shootTwice) {
		this.shootTwice = shootTwice;
	}

	public void ShootTheArrow () {
		// If the left mouse button is pressed
		if (Input.GetMouseButtonDown (0)) {
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