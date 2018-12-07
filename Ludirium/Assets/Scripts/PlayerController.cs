using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private static readonly int MAX_ITEMS_HELD = 3;
    public bool canInteract = false;   // true if currently can interact with some object
    public int numItemsHeld = 0;
    GameObject interactable;
    //public GameObject[] inventory = new GameObject[3];
    public Statics.Items[] inventory = new Statics.Items[MAX_ITEMS_HELD];
    public GameObject[] actualItems = new GameObject[MAX_ITEMS_HELD];
    public Transform[] spawnPoints;

    private ComponentController currComp;
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
            currComp = interactable.GetComponent<ComponentController>();
            if (currComp.isBroken)
            {
                CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
            }
        } else if (other.tag == "Item") {
            canInteract = true;
            if (numItemsHeld == MAX_ITEMS_HELD) {
                CanvasManager.Instance.ShowBottomLeftText("Inventory full!");
            } else {
                interactable = other.gameObject;
                CanvasManager.Instance.ShowBottomLeftText("Press Q to pick up");
            }
        } else if (other.tag == "Recycling") {
            canInteract = true;
            if (numItemsHeld != 0) {
                interactable = other.gameObject;
                CanvasManager.Instance.ShowBottomLeftText("Press Q to dump all items");
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
		if (canInteract && interactable != null && Input.GetKeyDown(KeyCode.Q)) {
            // do the interaction shit here
            if (interactable.tag == "Item" && inventory[MAX_ITEMS_HELD - 1] == Statics.Items.NONE) {
                for (int i = 0; i < MAX_ITEMS_HELD; i++) {
                    if (inventory[i] == Statics.Items.NONE) {
                        inventory[i] = interactable.GetComponent<Factory>().item;
                        switch (inventory[i]) {
                            case Statics.Items.NONE:
                                break;
                            case Statics.Items.GEAR:
                                actualItems[i] = Instantiate(ItemManager.Instance.gear);
                                break;
                            case Statics.Items.SPRING:
                                actualItems[i] = Instantiate(ItemManager.Instance.spring);
                                break;
                            case Statics.Items.SCREW:
                                actualItems[i] = Instantiate(ItemManager.Instance.screw);
                                break;
                            case Statics.Items.OIL:
                                //do some oily shit
                                interactable.GetComponent<Factory>().TakeItem();
                                break;
                        }
                        if (actualItems[i] != null) {
                            interactable.GetComponent<Factory>().TakeItem();
                            actualItems[i].transform.parent = spawnPoints[i].transform;
                            actualItems[i].transform.position = spawnPoints[i].transform.position;
                            numItemsHeld += 1;
                        }
                        break;
                    }
                }
            }

            else if (interactable.tag == "Component" && currComp.isBroken)
            {
                //Debug.Log("Attempting Repair");
                currComp.repair(inventory);
            }

            else if (interactable.tag == "Recycling" && inventory[0] != Statics.Items.NONE) {
                for (int i = 0; i < MAX_ITEMS_HELD; i++) {
                    inventory[i] = Statics.Items.NONE;
                    Destroy(actualItems[i]);
                }
                numItemsHeld = 0;
            }
        }
	}
}
