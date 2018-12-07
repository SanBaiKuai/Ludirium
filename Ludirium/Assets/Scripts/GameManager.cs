using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] componentList;

    private int updateTime = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time>= updateTime)
        {
            updateTime = Mathf.FloorToInt(Time.time) + 1;
            foreach (GameObject component in componentList)
            {
                component.GetComponent<ComponentController>().decay();
            }
        }
		
	}
}
