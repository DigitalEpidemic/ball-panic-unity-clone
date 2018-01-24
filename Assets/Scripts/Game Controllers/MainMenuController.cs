using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private Animator settingsButtonAnim;

	private bool hidden;
	private bool canTouchSettingsButton;

	[SerializeField]
	private Button musicBtn;

	[SerializeField]
	private Sprite[] musicBtnSprites;

	[SerializeField]
	private Button fbBtn;

	[SerializeField]
	private Sprite[] fbBtnSprites;

	[SerializeField]
	private GameObject infoPanel;

	[SerializeField]
	private Image infoImage;

	[SerializeField]
	private Sprite[] infoSprites;

	private int infoIndex;

	void Start () {
		canTouchSettingsButton = true;
		hidden = true;

		if (GameController.instance.isMusicOn) {
			musicBtn.image.sprite = musicBtnSprites [0];
		} else {
			musicBtn.image.sprite = musicBtnSprites [1];
		}

		infoIndex = 0;
		infoImage.sprite = infoSprites [infoIndex];
	} // Start
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SettingsButton () {
		StartCoroutine (DisableSettingsButtonWhilePlayingAnimation ());
	}

	IEnumerator DisableSettingsButtonWhilePlayingAnimation () {
		if (canTouchSettingsButton) {
			if (hidden) {
				canTouchSettingsButton = false;
				settingsButtonAnim.Play ("SlideIn");
				hidden = false;
				yield return new WaitForSeconds (1.2f);
				canTouchSettingsButton = true;

			} else {
				canTouchSettingsButton = false;
				settingsButtonAnim.Play ("SlideOut");
				hidden = true;
				yield return new WaitForSeconds (1.2f);
				canTouchSettingsButton = true;
			}
		}
	} // DisableSettingsButtonWhilePlayingAnimation

	public void MusicButton () {
		
		if (GameController.instance.isMusicOn) {
			musicBtn.image.sprite = musicBtnSprites [1];

			GameController.instance.isMusicOn = false;
			GameController.instance.Save ();

		} else {
			musicBtn.image.sprite = musicBtnSprites [0];

			GameController.instance.isMusicOn = true;
			GameController.instance.Save ();
		}
	} // MusicButton

	public void OpenInfoPanel () {
		infoPanel.SetActive (true);
	}

	public void CloseInfoPanel () {
		infoPanel.SetActive (false);
	}

	public void NextInfo () {
		infoIndex++;

		if (infoIndex == infoSprites.Length) {
			infoIndex = 0;
		}

		infoImage.sprite = infoSprites [infoIndex];
	}

} // MainMenuController