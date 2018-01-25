using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

	public static LoadingScreen instance;

	[SerializeField]
	GameObject bgImage, logoImage, text;

	void Awake () {
		MakeSingleton ();
	}
	
	void MakeSingleton () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	public void PlayLoadingScreen () {
		StartCoroutine (ShowLoadingScreen ());
	}

	IEnumerator ShowLoadingScreen () {
		Show ();
		yield return new WaitForSeconds (1f);
		Hide ();

		// Call Gameplay controller to begin game
	}

	void Show () {
		bgImage.SetActive (true);
		logoImage.SetActive (true);
		text.SetActive (true);
	}

	void Hide () {
		bgImage.SetActive (false);
		logoImage.SetActive (false);
		text.SetActive (false);
	}

} // LoadingScreen