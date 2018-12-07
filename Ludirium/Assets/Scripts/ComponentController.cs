using System.Collections;
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
                            if (k == PlayerController.Instance.numItemsHeld - 1) {
                                PlayerController.Instance.inventory[k] = Statics.Items.NONE;
                                PlayerController.Instance.actualItems[k] = null;
                                break;
                            }
                        }
                        PlayerController.Instance.numItemsHeld--;

                        currRepairItems[i] = Statics.Items.NONE;
                        currRepairItemsCount--;
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

    public void decay()
    {
        if (!isBroken)
        {
            health -= decayRate;
            if (health <= 0)
            {
                health = 0;
                isBroken = true;
                currRepairItemsCount = Random.Range(1, repairItems.Length + 1);
                for(int i = 0; i < currRepairItemsCount; i++)
                {
                    currRepairItems[i] = repairItems[Random.Range(0, repairItems.Length)];
                }
            }
        }
    }
}
