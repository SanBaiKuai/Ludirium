using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void Break() {
        anim.SetBool("Broken", true);
    }

    public void Fix() {
        anim.SetBool("Broken", false);
    }
}
