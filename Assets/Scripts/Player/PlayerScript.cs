using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private float speed = 8.0f;
	private float maxVelocity = 4.0f;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private Animator animator;

	void Awake () {
	}

	void Start () {
		
	}

	void Update () {
	}

	void FixedUpdate () {
		PlayerWalkKeyboard ();
	}

	void PlayerWalkKeyboard () {
		float force = 0.0f;
		float velocity = Mathf.Abs (myRigidBody.velocity.x);

		float h = Input.GetAxis ("Horizontal");

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

} // PlayerScript