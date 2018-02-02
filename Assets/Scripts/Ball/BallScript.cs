using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]

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

	public float x, y;

	[SerializeField]
	private GameObject[] collectableItems;

	void Awake () {
		if (this.gameObject.tag == "SmallestBall") {
			GameplayController.smallBallsCount++;
		}

		SetBallSpeed ();
		InstantiateBalls ();
	}

	void OnEnable () {
		PlayerScript.explode += Explode;
	}

	void OnDisable () {
		PlayerScript.explode -= Explode;
	}

	void Start () {
		if (!GameplayController.instance.levelInProgress) {
			transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (x, y, 5));
		}
	}

	void Update () {
		MoveBall ();
//		transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (x, y, 5));
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

		if (gameObject.tag != "SmallestBall") {
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
		}
			
		AudioSource.PlayClipAtPoint (popSounds [Random.Range (0, popSounds.Length)], transform.position);
		InitializeCollectableItems (transform.position);
		GiveScoreAndCoins (this.gameObject.tag);
		gameObject.SetActive (false);
	}

	public void Explode (bool touchedGoldBall) {
		StartCoroutine (ExplodeBalls (touchedGoldBall));
	}

	IEnumerator ExplodeBalls (bool touchedGoldBall) {
		if (this.gameObject.tag == "LargestBall") {
			yield return null;
		} else {
			yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (0.5f));
		}
			

		if (gameObject.tag != "SmallestBall") {
			Vector3 position = transform.position;

			ball1.transform.position = position;
			ball1Script.SetMoveLeft (true);

			ball2.transform.position = position;
			ball2Script.SetMoveRight (true);

			ball1.SetActive (true);
			ball2.SetActive (true);

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
		}

		if (touchedGoldBall) {
			if (this.gameObject.tag != "SmallestBall") {
				ball1Script.Explode (true);
				ball2Script.Explode (true);
			} else {
				GameplayController.instance.CountSmallBalls ();
			}

			this.gameObject.SetActive (false);
		} else {
			if (this.gameObject.tag != "SmallestBall") {
				ball1Script.Explode (false);
				ball2Script.Explode (false);
				this.gameObject.SetActive (false);
			}
		}
	}

	void GiveScoreAndCoins (string objTag) {
		switch (objTag) {
		case "LargestBall":
			GameplayController.instance.coins += Random.Range (15, 20);
			GameplayController.instance.playerScore += Random.Range (600, 700);
			break;
		case "LargeBall":
			GameplayController.instance.coins += Random.Range (13, 18);
			GameplayController.instance.playerScore += Random.Range (500, 600);
			break;
		case "MediumBall":
			GameplayController.instance.coins += Random.Range (11, 16);
			GameplayController.instance.playerScore += Random.Range (400, 500);
			break;
		case "SmallBall":
			GameplayController.instance.coins += Random.Range (10, 15);
			GameplayController.instance.playerScore += Random.Range (300, 400);
			break;
		case "SmallestBall":
			GameplayController.instance.coins += Random.Range (9, 14);
			GameplayController.instance.playerScore += Random.Range (200, 300);
			break;
		}
	}

	void InitializeCollectableItems (Vector3 position) {
		if (this.gameObject.tag != "SmallestBall") {
			int chance = Random.Range (0, 60);

			if (chance >= 0 && chance < 21) {
				Instantiate (collectableItems [Random.Range (4, collectableItems.Length)], position, Quaternion.identity);
			} else if (chance >= 21 && chance < 36) {
				Instantiate (collectableItems [Random.Range (0, 4)], position, Quaternion.identity);
			}

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
		if (target.tag == "FirstArrow" || target.tag == "SecondArrow" || target.tag == "FirstStickyArrow" || target.tag == "SecondStickyArrow") {
			if (gameObject.tag != "SmallestBall") {
				InitializeBallsAndTurnOffCurrentBall ();
			} else {
				AudioSource.PlayClipAtPoint (popSounds [Random.Range (0, popSounds.Length)], transform.position);
				GameplayController.instance.CountSmallBalls ();
				gameObject.SetActive (false);
			}
		} // If the ball hits the arrow

		if (target.tag == "UnbreakableBrickTop" || target.tag == "BrokenBrickTop" || target.tag == "UnbreakableBrickTopVertical") {
			myRigidBody.velocity = new Vector2 (0, 5);
		} else if (target.tag == "UnbreakableBrickBottom" || target.tag == "BrokenBrickBottom" || target.tag == "UnbreakableBrickBottomVertical") {
			myRigidBody.velocity = new Vector2 (0, -2);
		} else if (target.tag == "UnbreakableBrickLeft" || target.tag == "BrokenBrickLeft" || target.tag == "UnbreakableBrickLeftVertical") {
			moveLeft = true;
			moveRight = false;
		} else if (target.tag == "UnbreakableBrickRight" || target.tag == "BrokenBrickRight" || target.tag == "UnbreakableBrickRightVertical") {
			moveRight = true;
			moveLeft = false;
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
		if (target.tag == "Player") {
			if (PlayerScript.instance.hasShield) {
				PlayerScript.instance.DestroyShield ();
			} else {
				if (!PlayerScript.instance.isInvincible) {
					Destroy (target.gameObject);
					GameplayController.instance.PlayerDied ();
				}
			}
		}
	}

} // BallScript