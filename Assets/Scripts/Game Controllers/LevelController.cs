using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public Text scoreText, coinText;

	// Which level is unlocked
	public bool[] levels;

	public Button[] levelButtons;
	public Text[] levelText;
	public Image[] lockIcons;

	public GameObject coinShopPanel;

	// Use this for initialization
	void Start () {
		InitializeLevelMenu ();
	}

	void InitializeLevelMenu () {
		scoreText.text = "" + GameController.instance.highScore;
		coinText.text = "" + GameController.instance.coins;

		levels = GameController.instance.levels;

		for (int i = 1; i < levels.Length; i++) {
			if (levels [i]) {
				lockIcons [i - 1].gameObject.SetActive (false);
			} else {
				levelButtons [i - 1].interactable = false;
				levelText [i - 1].gameObject.SetActive (false);
			}
		}
	}

	public void LoadLevel () {
		if (GameController.instance.isMusicOn) {
			MusicController.instance.GameIsLoadedTurnOffMusic ();
		}

		string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

		switch (level) {
		case "Level0":
			GameController.instance.currentLevel = 0;
			break;
		case "Level1":
			GameController.instance.currentLevel = 1;
			break;
		case "Level2":
			GameController.instance.currentLevel = 2;
			break;
		case "Level3":
			GameController.instance.currentLevel = 3;
			break;
		case "Level4":
			GameController.instance.currentLevel = 4;
			break;
		case "Level5":
			GameController.instance.currentLevel = 5;
			break;
		case "Level6":
			GameController.instance.currentLevel = 6;
			break;
		case "Level7":
			GameController.instance.currentLevel = 7;
			break;
		case "Level8":
			GameController.instance.currentLevel = 8;
			break;
		case "Level9":
			GameController.instance.currentLevel = 9;
			break;
		case "Level10":
			GameController.instance.currentLevel = 10;
			break;
		case "Level11":
			GameController.instance.currentLevel = 11;
			break;
		case "Level12":
			GameController.instance.currentLevel = 12;
			break;
		case "Level13":
			GameController.instance.currentLevel = 13;
			break;
		case "Level14":
			GameController.instance.currentLevel = 14;
			break;
		case "Level15":
			GameController.instance.currentLevel = 15;
			break;
		case "Level16":
			GameController.instance.currentLevel = 16;
			break;
		case "Level17":
			GameController.instance.currentLevel = 17;
			break;
		case "Level18":
			GameController.instance.currentLevel = 18;
			break;
		case "Level19":
			GameController.instance.currentLevel = 19;
			break;
		case "Level20":
			GameController.instance.currentLevel = 20;
			break;
		case "Level21":
			GameController.instance.currentLevel = 21;
			break;
		case "Level22":
			GameController.instance.currentLevel = 22;
			break;
		case "Level23":
			GameController.instance.currentLevel = 23;
			break;
		case "Level24":
			GameController.instance.currentLevel = 24;
			break;
		case "Level25":
			GameController.instance.currentLevel = 25;
			break;
		case "Level26":
			GameController.instance.currentLevel = 26;
			break;
		case "Level27":
			GameController.instance.currentLevel = 27;
			break;
		case "Level28":
			GameController.instance.currentLevel = 28;
			break;
		case "Level29":
			GameController.instance.currentLevel = 29;
			break;
		case "Level30":
			GameController.instance.currentLevel = 30;
			break;
		case "Level31":
			GameController.instance.currentLevel = 31;
			break;
		case "Level32":
			GameController.instance.currentLevel = 32;
			break;
		case "Level33":
			GameController.instance.currentLevel = 33;
			break;
		case "Level34":
			GameController.instance.currentLevel = 34;
			break;
		case "Level35":
			GameController.instance.currentLevel = 35;
			break;
		case "Level36":
			GameController.instance.currentLevel = 36;
			break;
		case "Level37":
			GameController.instance.currentLevel = 37;
			break;
		case "Level38":
			GameController.instance.currentLevel = 38;
			break;
		case "Level39":
			GameController.instance.currentLevel = 39;
			break;
		}

		LoadingScreen.instance.PlayLoadingScreen ();
		SceneManager.LoadScene (level);
	}

	public void OpenCoinShop () {
		coinShopPanel.SetActive (true);
	}

	public void CloseCoinShop () {
		coinShopPanel.SetActive (false);
	}

	public void GoToMainMenu () {
		SceneManager.LoadScene ("MainMenu");
	}

	public void GoBackButton () {
		SceneManager.LoadScene ("PlayerMenu");
	}

} // LevelController