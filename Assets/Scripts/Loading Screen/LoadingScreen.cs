using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

	public static LoadingScreen instance;

	[SerializeField]
	GameObject bgImage, logoImage, text, fadePanel;

	[SerializeField]
	Animator fadeAnim;

	void Awake () {
		MakeSingleton ();
		Hide ();
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

	public void PlayFadeInAnimation () {
		StartCoroutine (FadeIn ());
	}

	IEnumerator FadeIn () {
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (0.4f));

		if (GameplayController.instance != null) {
			GameplayController.instance.setHasLevelStarted (true);
		}

		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (0.9f));
		fadePanel.SetActive (false);
	}

	public void FadeOut () {
		fadePanel.SetActive (true);
		fadeAnim.Play ("FadeOut");
	}

	IEnumerator ShowLoadingScreen () {
		Show ();
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (1f));
		Hide ();

		if (GameplayController.instance != null) {
			GameplayController.instance.setHasLevelStarted (true);
		}
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