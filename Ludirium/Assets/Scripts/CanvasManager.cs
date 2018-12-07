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
    private GameObject pauseTextObj;
    public bool paused;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		if (PlayerPrefs.HasKey("bestTime")) {
            // set best time text
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // call this from gamemanager
    public void UpdateCurrTime(int currTime) {
        // set curr time text
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
}
