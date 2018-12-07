using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gearbox : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Break() {
        anim.enabled = false;
    }

    public void Fix() {
        anim.enabled = true;
    }
}
