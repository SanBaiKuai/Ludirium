using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour {

    public static SmokeManager Instance { get; private set; }
    public GameObject smoke;

    private void Awake() {
        Instance = this;
    }

    public IEnumerator StartSmoke() {
        int numDivisions = (int)((PlayerController.Instance.timeToFix - 1.2) / 0.02f);
        for (int i = 0; i < numDivisions; i++) {
            Instantiate(smoke, new Vector3(Random.Range(-3f, 3f), Random.Range(-1f, 4f), 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
