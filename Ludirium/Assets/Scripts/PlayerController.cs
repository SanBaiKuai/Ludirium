using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool canInteract = false;   // true if currently can interact with some object
    GameObject interactable;
    public GameObject[] inventory = new GameObject[3];
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Component") {
            canInteract = true;
            interactable = other.gameObject;
            CanvasManager.Instance.ShowBottomLeftText("Press Q to repair");
        } else if (other.tag == "Factory") {
            canInteract = true;
            interactable = other.gameObject;
            CanvasManager.Instance.ShowBottomLeftText("Press Q to pick up");
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
                for (int i = 0; i < 3; i++) {
                    if (inventory[i] == null) {
                        inventory[i] = interactable.GetComponent<Factory>().item;
                        inventory[i].transform.position = spawnPoints[i].transform.position;
                        Instantiate(inventory[i]);
                        break;
                    }
                }
            }
        }
	}
}
