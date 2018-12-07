using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpringController : MonoBehaviour {
    public GameManager gm;

    private ComponentController componentScript;

	// Use this for initialization
	void Start () {
        componentScript = this.gameObject.GetComponent<ComponentController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
