using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenuController : MonoBehaviour {

	public static ShopMenuController instance;

	public Text coinText, scoreText, buyArrowsText, watchVideoText;

	public Button weaponsTabBtn, specialTabBtn, earnCoinsTabBtn, yesBtn;

	public GameObject weaponItemsPanel, specialItemsPanel, earnCoinsItemsPanel, coinShopPanel, buyArrowsPanel;

	void Awake () {
		MakeInstance ();
	}

	void Start () {
		InitializeShopMenuController ();
	}
	
	void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void BuyDoubleArrows () {
		if (!GameController.instance.weapons [1]) {
			if (GameController.instance.coins >= 7000) {
				buyArrowsPanel.SetActive (true);
				buyArrowsText.text = "Do You Want To Purchase Double Arrows?";
				yesBtn.onClick.RemoveAllListeners ();
				yesBtn.onClick.AddListener (() => BuyArrow (1));

			} else {
				buyArrowsPanel.SetActive (true);
				buyArrowsText.text = "You Don't Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners ();
				yesBtn.onClick.AddListener (() => OpenCoinShop ());
			}
		}
	} // BuyDoubleArrows

	public void BuyStickyArrow () {
		if (!GameController.instance.weapons [2]) {
			if (GameController.instance.coins >= 7000) {
				buyArrowsPanel.SetActive (true);
				buyArrowsText.text = "Do You Want To Purchase Sticky Arrow?";
				yesBtn.onClick.RemoveAllListeners ();
				yesBtn.onClick.AddListener (() => BuyArrow (2));

			} else {
				buyArrowsPanel.SetActive (true);
				buyArrowsText.text = "You Don't Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners ();
				yesBtn.onClick.AddListener (() => OpenCoinShop ());
			}
		}
	} // BuyStickyArrows

	public void BuyDoubleStickyArrows () {
		if (!GameController.instance.weapons [3]) {
			if (GameController.instance.coins >= 7000) {
				buyArrowsPanel.SetActive (true);
				buyArrowsText.text = "Do You Want To Purchase Double Sticky Arrows?";
				yesBtn.onClick.RemoveAllListeners ();
				yesBtn.onClick.AddListener (() => BuyArrow (3));

			} else {
				buyArrowsPanel.SetActive (true);
				buyArrowsText.text = "You Don't Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners ();
				yesBtn.onClick.AddListener (() => OpenCoinShop ());
			}
		}
	} // BuyDoubleStickyArrows

	public void BuyArrow (int index) {
		GameController.instance.weapons [index] = true;
		GameController.instance.coins -= 7000;
		GameController.instance.Save ();

		buyArrowsPanel.SetActive (false);
		coinText.text = "" + GameController.instance.coins;
	}

	void InitializeShopMenuController () {
		coinText.text = "" + GameController.instance.coins;
		scoreText.text = "" + GameController.instance.highScore;
	}

	public void OpenCoinShop () {
		if (buyArrowsPanel.activeInHierarchy) {
			buyArrowsPanel.SetActive (false);
		}

		coinShopPanel.SetActive (true);
	}

	public void CloseCoinShop () {
		coinShopPanel.SetActive (false);
	}

	public void OpenWeaponItemsPanel () {
		weaponItemsPanel.SetActive (true);
		specialItemsPanel.SetActive (false);
		earnCoinsItemsPanel.SetActive (false);
	}

	public void OpenSpecialItemsPanel () {
		specialItemsPanel.SetActive (true);
		weaponItemsPanel.SetActive (false);
		earnCoinsItemsPanel.SetActive (false);
	}

	public void OpenEarnCoinsItemsPanel () {
		earnCoinsItemsPanel.SetActive (true);
		weaponItemsPanel.SetActive (false);
		specialItemsPanel.SetActive (false);
	}

	public void PlayGame () {
		MusicController.instance.PlayClickClip ();
		SceneManager.LoadScene ("PlayerMenu");
	}

	public void GoToMainMenu () {
		MusicController.instance.PlayClickClip ();
		SceneManager.LoadScene ("MainMenu");
	}

	public void DontBuyArrows () {
		buyArrowsPanel.SetActive (false);
	}

} // ShopMenuController