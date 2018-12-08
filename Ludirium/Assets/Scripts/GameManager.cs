using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameObject[] componentList;
    public float energyStored;
    public float decayRate;
    public bool gameOver;
    public bool MainSpringBroken;

    public int updateTime = 1;
    private int brokenItems;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        updateTime = 1;
        Time.timeScale = 1;
        AudioManager.Instance.am.SetFloat("BGMVol", 0f);
        energyStored = 100f;
        decayRate = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver) {
            if (energyStored <= 0) {
                if (!PlayerPrefs.HasKey("bestTime") || PlayerPrefs.GetInt("bestTime") < updateTime) {
                    PlayerPrefs.SetInt("bestTime", updateTime);
                }
                StartCoroutine(AudioManager.Instance.PlayGameOver());
                CanvasManager.Instance.GameOver();
                gameOver = true;
                Time.timeScale = 0;
            }
            if (Time.time >= updateTime) {
                brokenItems = 0;
                updateTime = Mathf.FloorToInt(Time.time) + 1;
                foreach (GameObject component in componentList) {
                    brokenItems += component.GetComponent<ComponentController>().decay();
                }
                energyStored -= decayRate * (1.0f + (1.5f * brokenItems));
                CanvasManager.Instance.UpdateCurrTime(updateTime);
            }
        }
	}

    public void windUp()
    {
        energyStored = 100f;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
