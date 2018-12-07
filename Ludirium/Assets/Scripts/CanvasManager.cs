using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager Instance {get; private set; }
    public Text bottomLeftText;
    public Text currTime;
    public Text bestTime;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		if (PlayerPrefs.HasKey("bestTime")) {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
