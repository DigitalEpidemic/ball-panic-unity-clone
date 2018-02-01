using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour {

	private Rigidbody2D myRigidBody;

	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();

		if (this.gameObject.tag != "InGameCollectable") {
			Invoke ("DeactivateGameObject", Random.Range (2, 6));
		}
	}

	void DeactivateGameObject () {
		this.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.tag == "BottomBrick") {
			Vector3 temp = target.transform.position;
			temp.y += 0.8f;
			transform.position = new Vector2 (transform.position.x, temp.y);
			myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
		}

		if (target.tag == "Player") {
			if (this.gameObject.tag == "InGameCollectable") {
				GameController.instance.collectedItems [GameController.instance.currentLevel] = true;
				GameController.instance.Save ();

				if (GameplayController.instance != null) {
					if (GameController.instance.currentLevel == 0) {
						GameplayController.instance.playerScore += 1 * 1000;
					} else {
						GameplayController.instance.playerScore += GameController.instance.currentLevel * 1000;
					}
				}
			}
			this.gameObject.SetActive (false);
		}
	}

} // CollectablesScript