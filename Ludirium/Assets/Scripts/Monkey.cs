using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Monkey : MonoBehaviour {

    private Animator anim;
    private ComponentController cc;
    bool isBroken;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        cc = GetComponent<ComponentController>();
    }

    void Update()
    {
        if (cc.isBroken != isBroken) {
            if (cc.isBroken) {
                Break();
            } else {
                StartCoroutine(Fix());
            }
            isBroken = cc.isBroken;
        }
    }

    public void Break() {
        anim.SetBool("Broken", true);
        StartCoroutine(TurnDown());
    }

    public IEnumerator Fix() {
        StartCoroutine(TurnUp());
        yield return new WaitForSeconds(PlayerController.Instance.timeToFix);
        anim.SetBool("Broken", false);
    }

    IEnumerator TurnUp() {
        for (int i = 9; i >= 0; i--) {
            AudioManager.Instance.am.SetFloat("BGMVol", i * (-5));
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator TurnDown() {
        for (int i = 0; i < 20; i++) {
            AudioManager.Instance.am.SetFloat("BGMVol", i * (-2.5f));
            yield return new WaitForSeconds(0.1f);
        }
        AudioManager.Instance.am.SetFloat("BGMVol", -80f);
    }
}
