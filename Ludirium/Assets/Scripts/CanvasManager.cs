using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager Instance {get; private set; }
    public Text bottomLeftText;
    public Text currTime;
    public Text bestTime;
    public GameObject pauseText;
    public Image maskFull;
    public Image maskCircle;
    private GameObject pauseTextObj;
    public bool paused;
    public Slider windSlider;
    public Button pauseButton;
    public GameObject gameOverMenu;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		if (PlayerPrefs.HasKey("bestTime")) {
            // set best time text
            bestTime.text = "Best Time\n" + PlayerPrefs.GetInt("bestTime") / 60 + ":" + (PlayerPrefs.GetInt("bestTime") % 60).ToString("00");
        }
        else {
            bestTime.text = "Best Time\n--:--";
        }
	}
	
	// Update is called once per frame
	void Update () {
        windSlider.value = GameManager.Instance.energyStored;
        if (GameManager.Instance.energyStored <= 25) {
            windSlider.GetComponentInChildren<Image>().color = Color.red;
        } else {
            windSlider.GetComponentInChildren<Image>().color = Color.green;
        }
	}

    // call this from gamemanager
    public void UpdateCurrTime(int currTimeTime) {
        // set curr time text
        currTime.text = "" + currTimeTime/60 + ":" + (currTimeTime%60).ToString("00");
    }

    public void ShowBottomLeftText() {
        bottomLeftText.enabled = true;
    }

    public void ShowBottomLeftText(string msg) {
        bottomLeftText.text = msg;
        ShowBottomLeftText();
    }

    public void HideBottomLeftText() {
        bottomLeftText.enabled = false;
    }

    private void DisplayPauseText(bool state) {
        if (state) {
            pauseTextObj = (GameObject)Instantiate(pauseText);
            pauseTextObj.GetComponent<RectTransform>().SetParent(this.gameObject.transform, false);
        } else {
            Destroy(pauseTextObj);
        }
    }

    public void TogglePause() {
        paused = !paused;
        DisplayPauseText(paused);
        if (paused) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void TurnDark() {
        maskCircle.enabled = true;
        maskFull.enabled = true;
    }

    public void TurnBright() {
        maskCircle.enabled = false;
        maskFull.enabled = false;
    }

    public void GameOver() {
        pauseButton.interactable = false;
        gameOverMenu.SetActive(true);
    }
}
