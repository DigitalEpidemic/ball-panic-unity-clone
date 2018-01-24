using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {
	
	public static MusicController instance;

	private AudioSource bgMusic, click;

	private float time;


	void Awake () {
		MakeSingleton ();

		AudioSource[] audioSources = GetComponents<AudioSource> ();

		bgMusic = audioSources [0];
		click = audioSources [1];
	}

	void MakeSingleton () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void OnEnable () {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable () {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (scene.name == "LevelMenu") {
			if (GameController.instance.isMusicOn) {
				if (!bgMusic.isPlaying) {
					bgMusic.time = time;
					bgMusic.Play ();
				}
			}
		}
	}

	public void GameIsLoadedTurnOffMusic () {
		if (bgMusic.isPlaying) {
			time = bgMusic.time;
			bgMusic.Stop ();
		}
	}

	public void PlayBGMusic () {
		if (!bgMusic.isPlaying) {
			bgMusic.Play ();
		}
	}

	public void StopBGMusic () {
		if (bgMusic.isPlaying) {
			bgMusic.Stop ();
		}
	}

	public void PlayClickClip () {
		click.Play ();
	}

} // MusicController