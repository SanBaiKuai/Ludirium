﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamo : MonoBehaviour {

    SpriteRenderer sr;
    public Sprite on;
    public Sprite off;
    private ComponentController cc;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<ComponentController>();
    }

    void Update()
    {
        if (cc.isBroken)
        {
            Break();
        }
        else
        {
            StartCoroutine(Fix());
        }

    }

    public void Break() {
        sr.sprite = off;
        CanvasManager.Instance.TurnDark();
    }

    public IEnumerator Fix() {
        yield return new WaitForSeconds(PlayerController.Instance.timeToFix);
        sr.sprite = on;
        CanvasManager.Instance.TurnBright();
    }
}
