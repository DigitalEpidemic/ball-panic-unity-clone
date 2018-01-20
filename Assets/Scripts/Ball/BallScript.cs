using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	private float forceX, forceY;

	[SerializeField]
	private bool moveLeft, moveRight;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	void Awake () {
		SetBallSpeed ();
	}

	void Start () {
		
	}

	void Update () {
		MoveBall ();
	}

	void MoveBall () {
		if (moveLeft) {
			Vector3 temp = transform.position;
			temp.x -= (forceX * Time.deltaTime);
			transform.position = temp;
		}

		if (moveRight) {
			Vector3 temp = transform.position;
			temp.x += (forceX * Time.deltaTime);
			transform.position = temp;
		}
	}

	void SetBallSpeed () {
		forceX = 2.5f;

		switch (this.gameObject.tag) {
		case "LargestBall":
			forceY = 11.5f;
			break;
		case "LargeBall":
			forceY = 10.5f;
			break;
		case "MediumBall":
			forceY = 9f;
			break;
		case "SmallBall":
			forceY = 8f;
			break;
		case "SmallestBall":
			forceY = 7f;
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.tag == "BottomBrick") {
			myRigidBody.velocity = new Vector2 (0, forceY);
		}

		if (target.tag == "LeftBrick") {
			moveLeft = false;
			moveRight = true;
		}

		if (target.tag == "RightBrick") {
			moveLeft = true;
			moveRight = false;
		}
	}

} // BallScript