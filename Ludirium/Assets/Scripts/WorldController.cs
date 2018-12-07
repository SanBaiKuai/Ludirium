﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public float speed = 50.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
        }

    }
}
