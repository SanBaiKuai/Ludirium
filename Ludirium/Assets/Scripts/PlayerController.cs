﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }
    private static readonly int MAX_ITEMS_HELD = 3;
    public bool canInteract = false;   // true if currently can interact with some object
    public int numItemsHeld = 0;
    public GameObject interactable;
    public GameObject prevInteractable;
    //public GameObject[] inventory = new GameObject[3];
    public Statics.Items[] inventory = new Statics.Items[MAX_ITEMS_HELD];
    public GameObject[] actualItems = new GameObject[MAX_ITEMS_HELD];
    public Transform[] spawnPoints;
    public float timeToFix = 2.5f;
    float nonOilySpeed = 30f;
    public bool isOily = false;

    public ComponentController currComp;

    private void Awake() {
        Instance = this;
    }

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
                bool canWeFixIt = false;
                foreach (Statics.Items item in currComp.currRepairItems) {
                    if (item != Statics.Items.NONE && !canWeFixIt) {
                        foreach (Statics.Items playerItem in inventory) {
                            if (item == playerItem) {
                                canWeFixIt = true;
                                break;
                            }
                        }
                    }
                }
                if (canWeFixIt) {
                    CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
                } else {
                    CanvasManager.Instance.ShowBottomLeftText("Unable to repair!");
                }
            }
        } else if (other.tag == "Item") {
            canInteract = true;
            prevInteractable = interactable;
            if (numItemsHeld == MAX_ITEMS_HELD && !other.name.Contains("Can")) {
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
        } else if (other.tag == "MainSpring")
        {
            canInteract = true;
            interactable = other.gameObject;
            if (interactable.GetComponent<Gearbox>().isBroken())
            {
                currComp = interactable.GetComponent<ComponentController>();
                bool canWeFixIt = false;
                foreach (Statics.Items item in currComp.currRepairItems) {
                    if (item != Statics.Items.NONE && !canWeFixIt) {
                        foreach (Statics.Items playerItem in inventory) {
                            if (item == playerItem) {
                                canWeFixIt = true;
                                break;
                            }
                        }
                    }
                }
                if (canWeFixIt) {
                    CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
                } else {
                    CanvasManager.Instance.ShowBottomLeftText("Unable to repair!");
                }
            } else
            {
                CanvasManager.Instance.ShowBottomLeftText("Press Q to wind the main spring");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (canInteract) {
            if (prevInteractable == null) {
                canInteract = false;
                interactable = null;
                currComp = null;
                CanvasManager.Instance.HideBottomLeftText();
            } else {    // it's definitely a component
                interactable = prevInteractable;
                prevInteractable = null;
                if (currComp != null && currComp.isBroken) {
                    CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
                } else {
                    CanvasManager.Instance.HideBottomLeftText();
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (!GameManager.Instance.gameOver) {
            if (canInteract && interactable != null && Input.GetKeyDown(KeyCode.Q)) {
                // do the interaction shit here
                if (interactable.tag == "Item") {
                    {
                        if (interactable.name.Contains("Can")) {
                            prevInteractable.GetComponent<Factory>().TakeItem();
                            interactable = prevInteractable;
                            CanvasManager.Instance.ShowBottomLeftText("Gotta go fast!");
                            interactable.GetComponent<Factory>().lastTime = GameManager.Instance.updateTime;
                            isOily = true;
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
                                        actualItems[i].GetComponent<BoxCollider>().enabled = false;
                                        actualItems[i].GetComponent<SpriteRenderer>().sortingLayerName = "HeldItems";
                                        actualItems[i].GetComponent<SpriteRenderer>().sortingOrder = i;
                                        numItemsHeld += 1;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                } else if (interactable.tag == "Component" && currComp.isBroken) {
                    currComp.repair();
                } else if (interactable.tag == "MainSpring") {
                    if (interactable.GetComponent<Gearbox>().isBroken()) {
                        currComp.repair();
                    } else {
                        interactable.GetComponent<Gearbox>().wind();
                    }
                } else if (interactable.tag == "Recycling" && inventory[0] != Statics.Items.NONE) {
                    for (int i = 0; i < MAX_ITEMS_HELD; i++) {
                        inventory[i] = Statics.Items.NONE;
                        Destroy(actualItems[i]);
                    }
                    numItemsHeld = 0;
                }
                nonOilySpeed = 30 - 5 * numItemsHeld;
            }
        }
        if (!isOily) {
            WorldController.Instance.speed = nonOilySpeed;
        }
    }

    IEnumerator SpeedUpCountDown() {
        yield return new WaitForSeconds(5f);
        WorldController.Instance.speed = nonOilySpeed;
    }

    public IEnumerator FixShit() {
        WorldController.Instance.canMove = false;
        for (int i = 0; i < 5; i++) {
            transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0, 4f), transform.position.z);
            transform.RotateAround(transform.position, Vector3.forward, Random.Range(-180f, 180f));
            yield return new WaitForSeconds(timeToFix/5);
        }
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        WorldController.Instance.canMove = true;
    }
}
