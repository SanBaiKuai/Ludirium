using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuTitle : MonoBehaviour {

    private void Update() {
        if (transform.position.y > 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
        }
    }
}
