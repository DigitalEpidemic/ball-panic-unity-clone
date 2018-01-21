using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	private float forceX, forceY;

	[SerializeField]
	private bool moveLeft, moveRight;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private GameObject originalBall;

	private GameObject ball1, ball2;
	private BallScript ball1Script, ball2Script;

	[SerializeField]
	private AudioClip[] popSounds;

	void Awake () {
		SetBallSpeed ();
		InstantiateBalls ();
	}

	void Start () {
		
	}

	void Update () {
		MoveBall ();
	}

	void InstantiateBalls () {
		if (this.gameObject.tag != "SmallestBall") {
			ball1 = Instantiate (originalBall);
			ball2 = Instantiate (originalBall);

			ball1Script = ball1.GetComponent<BallScript> ();
			ball2Script = ball2.GetComponent<BallScript> ();

			ball1.SetActive (false);
			ball2.SetActive (false);
		}
	}

	public void SetMoveLeft (bool moveLeft) {
		this.moveLeft = moveLeft;
		this.moveRight = !moveLeft;
	}

	public void SetMoveRight (bool moveRight) {
		this.moveLeft = !moveRight;
		this.moveRight = moveRight;
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

	void InitializeBallsAndTurnOffCurrentBall () {
		Vector3 position = transform.position;

		ball1.transform.position = position;
		ball1Script.SetMoveLeft (true);

		ball2.transform.position = position;
		ball2Script.SetMoveRight (true);

		ball1.SetActive (true);
		ball2.SetActive (true);

		switch (gameObject.tag) {
		case "LargestBall":
			if (transform.position.y > 1 && transform.position.y <= 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
			} else if (transform.position.y > 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
			} else if (transform.position.y < 1) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
			}
			break;

		case "LargeBall":
			if (transform.position.y > 1 && transform.position.y <= 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
			} else if (transform.position.y > 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
			} else if (transform.position.y < 1) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
			}
			break;
		case "MediumBall":
			if (transform.position.y > 1 && transform.position.y <= 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
			} else if (transform.position.y > 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
			} else if (transform.position.y < 1) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
			}
			break;
		case "SmallBall":
			if (transform.position.y > 1 && transform.position.y <= 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 3.5f);
			} else if (transform.position.y > 1.3f) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 2f);
			} else if (transform.position.y < 1) {
				ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
				ball2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 5.5f);
			}
			break;
		}
		AudioSource.PlayClipAtPoint (popSounds [Random.Range (0, popSounds.Length)], transform.position);
		gameObject.SetActive (false);
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

		// If the ball hits the arrow
		if (target.tag == "FirstArrow" || target.tag == "SecondArrow" || target.tag == "FirstStickyArrow" || target.tag == "SecondStickyArrow") {
			if (gameObject.tag != "SmallestBall") {
				InitializeBallsAndTurnOffCurrentBall ();
			} else {
				gameObject.SetActive (false);
			}
		}

		// If it hits any of the bricks
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