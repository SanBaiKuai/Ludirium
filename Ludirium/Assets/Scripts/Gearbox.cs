using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gearbox : MonoBehaviour {

    private Animator anim;
    private ComponentController cc;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        cc = GetComponent<ComponentController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cc.isBroken)
        {
            Break();
        } else
        {
            Fix();
        }
		
	}

    public void Break() {
        anim.enabled = false;
    }

    public void Fix() {
        anim.enabled = true;
    }
}
