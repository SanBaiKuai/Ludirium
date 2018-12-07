using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public static WorldController Instance { get; private set; }

    public float speed = 50.0f;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            PlayerController.Instance.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
            PlayerController.Instance.GetComponent<SpriteRenderer>().flipX = false;
        }

    }
}
