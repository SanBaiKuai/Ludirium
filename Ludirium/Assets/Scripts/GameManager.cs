using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameObject[] componentList;
    public float energyStored;
    public float decayRate;

    public int updateTime = 1;
    private int brokenItems;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        energyStored = 100f;
        decayRate = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if(energyStored <= 0)
        {
            if (!PlayerPrefs.HasKey("bestTime") || PlayerPrefs.GetInt("bestTime") < updateTime) {
                PlayerPrefs.SetInt("bestTime", updateTime);
            }
        }
        if(Time.time>= updateTime)
        {
            brokenItems = 0;
            updateTime = Mathf.FloorToInt(Time.time) + 1;
            foreach (GameObject component in componentList)
            {
                brokenItems += component.GetComponent<ComponentController>().decay();
            }
            energyStored -= decayRate * (1.0f + brokenItems);
            CanvasManager.Instance.UpdateCurrTime(updateTime);
        }
		
	}

    public void windUp()
    {
        energyStored = 100f;
    }
}
