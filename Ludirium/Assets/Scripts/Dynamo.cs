using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamo : MonoBehaviour {

    SpriteRenderer sr;
    public Sprite on;
    public Sprite off;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

    public void Break() {
        sr.sprite = off;
    }

    public void Fix() {
        sr.sprite = on;
    }
}
