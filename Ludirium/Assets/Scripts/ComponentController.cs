using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController : MonoBehaviour {
    public enum Items {None, Gear, Spring, Tape };
    public float decayRate;
    public float health;
    public Items repairItem;

    private bool isBroken;

	// Use this for initialization
	void Start () {
        isBroken = false;
        health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
	}

    //returns true if repair is successful, returns 0 otherwise
    public bool repair(Items item)
    {
        if (item == repairItem)
        {
            isBroken = false;
            health = 100f;
            return true;
        } else if (item == Items.Tape)
        {
            isBroken = false;
            health = 50f;
            return true;
        }
        return false;
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
            }
        }
    }
}
