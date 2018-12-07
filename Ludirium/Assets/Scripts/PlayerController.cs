using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private static readonly int MAX_ITEMS_HELD = 3;
    public bool canInteract = false;   // true if currently can interact with some object
    public int numItemsHeld = 0;
    GameObject interactable;
    GameObject prevInteractable;
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
            prevInteractable = interactable;
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
            if (prevInteractable == null) {
                canInteract = false;
                interactable = null;
                CanvasManager.Instance.HideBottomLeftText();
            } else {    // it's definitely a component
                interactable = prevInteractable;
                prevInteractable = null;
                CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if (canInteract && interactable != null && Input.GetKeyDown(KeyCode.Q)) {
            // do the interaction shit here
            if (interactable.tag == "Item") {
                {
                    if (interactable.name.Contains("Can")) {
                        prevInteractable.GetComponent<Factory>().TakeItem();
                        interactable = prevInteractable;
                        CanvasManager.Instance.ShowBottomLeftText("Gotta go fast!");
                        interactable.GetComponent<Factory>().lastTime = GameManager.Instance.updateTime;
                        WorldController.Instance.speed = 75f;
                        StartCoroutine("SpeedUpCountDown");
                    } else if (inventory[MAX_ITEMS_HELD - 1] == Statics.Items.NONE) {
                        for (int i = 0; i < MAX_ITEMS_HELD; i++) {
                            if (inventory[i] == Statics.Items.NONE) {
                                switch (interactable.name.Substring(0, 3)) {
                                    case "Gea":
                                        inventory[i] = Statics.Items.GEAR;
                                        actualItems[i] = Instantiate(ItemManager.Instance.gear);
                                        break;
                                    case "Spr":
                                        inventory[i] = Statics.Items.SPRING;
                                        actualItems[i] = Instantiate(ItemManager.Instance.spring);
                                        break;
                                    case "Scr":
                                        inventory[i] = Statics.Items.SCREW;
                                        actualItems[i] = Instantiate(ItemManager.Instance.screw);
                                        break;
                                }
                                if (actualItems[i] != null) {
                                    prevInteractable.GetComponent<Factory>().TakeItem();
                                    interactable = prevInteractable;
                                    CanvasManager.Instance.ShowBottomLeftText("Obtained " + interactable.GetComponent<Factory>().item);
                                    interactable.GetComponent<Factory>().lastTime = GameManager.Instance.updateTime;
                                    actualItems[i].transform.parent = spawnPoints[i].transform;
                                    actualItems[i].transform.position = spawnPoints[i].transform.position;
                                    numItemsHeld += 1;
                                }
                                break;
                            }
                        }
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

    IEnumerator SpeedUpCountDown() {
        yield return new WaitForSeconds(10f);
        WorldController.Instance.speed = 50f;
    }
}
