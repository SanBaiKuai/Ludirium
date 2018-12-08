using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = 3 * transform.position;
        StartCoroutine(KillSelf());
	}
	
    IEnumerator KillSelf() {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
