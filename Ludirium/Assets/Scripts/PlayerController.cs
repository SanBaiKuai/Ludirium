using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private static readonly int MAX_ITEMS_HELD = 3;
    public bool canInteract = false;   // true if currently can interact with some object
    public bool isFullLoad = false; // true if currently holding max number of items 
    GameObject interactable;
    //public GameObject[] inventory = new GameObject[3];
    public Statics.Items[] inventory = new Statics.Items[MAX_ITEMS_HELD];
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < inventory.Length; i++) {
            inventory[i] = Statics.Items.NONE;
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Component") {
            canInteract = true;
            interactable = other.gameObject;
            CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
        } else if (other.tag == "Factory") {
            canInteract = true;
            if (isFullLoad) {
                CanvasManager.Instance.ShowBottomLeftText("Inventory full!");
            } else {
                interactable = other.gameObject;
                CanvasManager.Instance.ShowBottomLeftText("Press Q to pick up");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (canInteract) {
            canInteract = false;
            interactable = null;
            CanvasManager.Instance.HideBottomLeftText();
        }
    }

    // Update is called once per frame
    void Update () {
		if (canInteract && Input.GetKeyDown(KeyCode.Q)) {
            // do the interaction shit here
            if (interactable.tag == "Factory") {
                for (int i = 0; i < MAX_ITEMS_HELD; i++) {
                    if (inventory[i] == Statics.Items.NONE) {
                        inventory[i] = interactable.GetComponent<Factory>().item;
                        GameObject newItem = null;
                        switch (inventory[i]) {
                            case Statics.Items.NONE:
                                break;
                            case Statics.Items.GEAR:
                                newItem = Instantiate(ItemManager.Instance.gear);
                                break;
                            case Statics.Items.SPRING:
                                newItem = Instantiate(ItemManager.Instance.spring);
                                break;
                            case Statics.Items.SCREW:
                                newItem = Instantiate(ItemManager.Instance.screw);
                                break;
                        }
                        if (newItem != null) {
                            newItem.transform.parent = spawnPoints[i].transform;
                            newItem.transform.position = spawnPoints[i].transform.position;
                        }
                        if (i == MAX_ITEMS_HELD - 1) {
                            isFullLoad = true;
                        }
                        break;
                    }
                }
            }
        }
	}
}
