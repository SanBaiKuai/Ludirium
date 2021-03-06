﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController : MonoBehaviour {
    public float decayRate;
    public float health;
    public int minTime = 5;
    public int maxTime = 15;
    public int currRepairItemsCount;
    public Statics.Items[] currRepairItems;
    public Statics.Items[] repairItems;
    public Transform[] positions = new Transform[2];
    public GameObject[] shownObjects = new GameObject[2];

    public bool isBroken;

	// Use this for initialization
	void Start () {
        isBroken = false;
        health = 100f;
        decayRate = Random.Range(minTime, maxTime);

    }
	
	// Update is called once per frame
	void Update () {
	}

    private void attemptRepair()
    {
        CanvasManager.Instance.HideBottomLeftText();
        Statics.Items[] currItems = PlayerController.Instance.inventory;
        for (int i = 0; i < currRepairItems.Length; i++)
        {
            Statics.Items repairItem = currRepairItems[i];
            if (repairItem != Statics.Items.NONE)
            {
                for (int j = 0; j < currItems.Length; j++)
                {
                    Statics.Items currItem = PlayerController.Instance.inventory[j];
                    if (repairItem == currItem)
                    {
                        StartCoroutine(PlayerController.Instance.FixShit());
                        StartCoroutine(SmokeManager.Instance.StartSmoke());
                        PlayerController.Instance.inventory[j] = Statics.Items.NONE;
                        Destroy(PlayerController.Instance.actualItems[j]);
                        for (int k = j + 1; k < PlayerController.Instance.numItemsHeld; k++) {
                            if (PlayerController.Instance.inventory[k] == Statics.Items.NONE) {
                                break;
                            }
                            PlayerController.Instance.inventory[k - 1] = PlayerController.Instance.inventory[k];
                            PlayerController.Instance.actualItems[k - 1] = PlayerController.Instance.actualItems[k];
                            PlayerController.Instance.actualItems[k - 1].transform.parent = PlayerController.Instance.spawnPoints[k - 1];
                            PlayerController.Instance.actualItems[k - 1].transform.position = PlayerController.Instance.spawnPoints[k - 1].position;
                            PlayerController.Instance.actualItems[k - 1].GetComponent<SpriteRenderer>().sortingOrder = k - 1;
                            if (k == PlayerController.Instance.numItemsHeld - 1) {
                                PlayerController.Instance.inventory[k] = Statics.Items.NONE;
                                PlayerController.Instance.actualItems[k] = null;
                                break;
                            }
                        }
                        PlayerController.Instance.numItemsHeld--;

                        currRepairItems[i] = Statics.Items.NONE;
                        currRepairItemsCount--;

                        Destroy(shownObjects[i]);

                        break;
                    }
                }
            }
        }
    }

    //returns true if repair is successful, returns 0 otherwise
    public void repair()
    {
        attemptRepair();
        if(currRepairItemsCount == 0)
            {
                isBroken = false;
                health = 100f;
                decayRate = Random.Range(minTime, maxTime); ;
            }
    }
    //returns 1 if broken, 0 otherwise
    public int decay()
    {
        if (!isBroken)
        {
            health -= decayRate;
            if (health <= 0)
            {
                health = 0;
                isBroken = true;
                currRepairItemsCount = Random.Range(1, Mathf.Min(3, repairItems.Length + 1));
                for(int i = 0; i < currRepairItemsCount; i++)
                {
                    currRepairItems[i] = repairItems[Random.Range(0, repairItems.Length)];
                    shownObjects[i] = Instantiate(ItemManager.Instance.GetNoBoxObjectFromEnum(currRepairItems[i]), positions[i]);
                    shownObjects[i].transform.position = positions[i].position;
                }
                return 1;
            }
            return 0;
        } else
        {
            return 1;
        }
    }
}
