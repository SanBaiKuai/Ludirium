using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMouse : MonoBehaviour {

    float lastUpdateTime = 0;
    int updateInterval = 1;

    private void Update() {
        if (Time.time > lastUpdateTime + updateInterval) {
            lastUpdateTime = Time.time;
            transform.position = new Vector3(Random.Range(-9, 9), Random.Range(-4, 4), 0);
            transform.Rotate(0, 0, Random.Range(-180, 180));
            float randomScale = Random.Range(0.25f, 1);
            transform.localScale = new Vector3(randomScale, randomScale, 1);
        }
    }
}
