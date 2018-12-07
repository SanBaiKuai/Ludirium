using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour {

    private Animator anim;
    private ComponentController cc;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
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
            Fix();
        }

    }

    public void Break() {
        anim.SetBool("Broken", true);
    }

    public void Fix() {
        anim.SetBool("Broken", false);
    }
}
