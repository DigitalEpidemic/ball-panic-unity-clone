using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour {

	public static GameController instance;
	private GameData data;

	public int currentLevel = -1;
	public int currentScore;
	public int currentLives;

	public bool isGameStartedFromLevelMenu;
	public bool isGameStartedFirstTime;
	public bool isMusicOn;
	public bool doubleCoins;

	public int selectedPlayer;
	public int selectedWeapon;
	public int coins;
	public int highScore;

	public bool[] players;
	public bool[] levels;
	public bool[] weapons;
	public bool[] achievements;
	public bool[] collectedItems;

	void Awake () {
		MakeSingleton ();
		InitializeGameVariables ();
	}

	void Start () {
		
	}

	void MakeSingleton () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void InitializeGameVariables () {
		Load ();

		if (data != null) {
			isGameStartedFirstTime = data.IsGameStartedFirstTime;
		} else {
			isGameStartedFirstTime = true;
		}

		if (isGameStartedFirstTime) {
			coins = 0;
			highScore = 0;

			selectedPlayer = 0;
			selectedWeapon = 0;

			isGameStartedFirstTime = false;
			isMusicOn = false;

			players = new bool[6];
			levels = new bool[40];
			weapons = new bool[4];
			achievements = new bool[8];
			collectedItems = new bool[40];

			players [0] = true;
			for (int i = 1; i < players.Length; i++) {
				players [i] = false;
			}

			levels [0] = true;
			for (int i = 1; i < levels.Length; i++) {
				levels [i] = false;
			}

			weapons [0] = true;
			for (int i = 1; i < weapons.Length; i++) {
				weapons [i] = false;
			}

			for (int i = 0; i < achievements.Length; i++) {
				achievements [i] = false;
			}

			for (int i = 0; i < collectedItems.Length; i++) {
				collectedItems [i] = false;
			}

			data = new GameData ();

			data.HighScore = highScore;
			data.Coins = coins;
			data.IsGameStartedFirstTime = isGameStartedFirstTime;
			data.Players = players;
			data.Levels = levels;
			data.Weapons = weapons;
			data.SelectedPlayer = selectedPlayer;
			data.SelectedWeapon = selectedWeapon;
			data.IsMusicOn = isMusicOn;
			data.Achievements = achievements;
			data.CollectedItems = collectedItems;

			Save ();
			Load ();

		} else {
			highScore = data.HighScore;
			coins = data.Coins;
			selectedPlayer = data.SelectedPlayer;
			selectedWeapon = data.SelectedWeapon;
			isGameStartedFirstTime = data.IsGameStartedFirstTime;
			isMusicOn = data.IsMusicOn;
			players = data.Players;
			levels = data.Levels;
			weapons = data.Weapons;
			achievements = data.Achievements;
			collectedItems = data.CollectedItems;
		}
	} // InitializeGameVariables

	public void Save () {
		FileStream file = null;

		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Create (Application.persistentDataPath + "/GameData.dat");

			if (data!= null) {
				data.HighScore = highScore;
				data.Coins = coins;
				data.IsGameStartedFirstTime = isGameStartedFirstTime;
				data.Players = players;
				data.Levels = levels;
				data.Weapons = weapons;
				data.SelectedPlayer = selectedPlayer;
				data.SelectedWeapon = selectedWeapon;
				data.IsMusicOn = isMusicOn;
				data.Achievements = achievements;
				data.CollectedItems = collectedItems;

				bf.Serialize (file, data);
			}

		} catch (Exception e) {
		} finally {
			if (file != null) {
				file.Close ();
			}
		}
	} // Save

	public void Load () {

		FileStream file = null;

		try {
			BinaryFormatter bf = new BinaryFormatter ();

			file = File.Open (Application.persistentDataPath + "/GameData.dat", FileMode.Open);
			data = (GameData)bf.Deserialize (file);

		} catch (Exception e) {
		} finally {
			if (file != null) {
				file.Close ();
			}
		}

	} // Load

} // GameController

[Serializable]
class GameData {
	
	private bool isGameStartedFirstTime;
	private bool isMusicOn;
	private bool doubleCoins;

	private int selectedPlayer;
	private int selectedWeapon;
	private int coins;
	private int highScore;

	private bool[] players;
	private bool[] levels;
	private bool[] weapons;
	private bool[] achievements;
	private bool[] collectedItems;


	// Getters and Setters

	public bool DoubleCoins {
		get {
			return this.doubleCoins;
		}
		set {
			doubleCoins = value;
		}
	}

	public bool IsGameStartedFirstTime {
		get {
			return this.isGameStartedFirstTime;
		}
		set {
			isGameStartedFirstTime = value;
		}
	}

	public bool IsMusicOn {
		get {
			return this.isMusicOn;
		}
		set {
			isMusicOn = value;
		}
	}

	public int SelectedPlayer {
		get {
			return this.selectedPlayer;
		}
		set {
			selectedPlayer = value;
		}
	}

	public int SelectedWeapon {
		get {
			return this.selectedWeapon;
		}
		set {
			selectedWeapon = value;
		}
	}

	public int Coins {
		get {
			return this.coins;
		}
		set {
			coins = value;
		}
	}

	public int HighScore {
		get {
			return this.highScore;
		}
		set {
			highScore = value;
		}
	}

	public bool[] Players {
		get {
			return this.players;
		}
		set {
			players = value;
		}
	}

	public bool[] Levels {
		get {
			return this.levels;
		}
		set {
			levels = value;
		}
	}

	public bool[] Weapons {
		get {
			return this.weapons;
		}
		set {
			weapons = value;
		}
	}

	public bool[] Achievements {
		get {
			return this.achievements;
		}
		set {
			achievements = value;
		}
	}

	public bool[] CollectedItems {
		get {
			return this.collectedItems;
		}
		set {
			collectedItems = value;
		}
	}

} // GameData