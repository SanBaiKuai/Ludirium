using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }
    AudioSource bgm;
    AudioSource gameOver;
    public AudioMixer am;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        bgm = GetComponents<AudioSource>()[0];
        gameOver = GetComponents<AudioSource>()[1];
    }

    public IEnumerator PlayGameOver() {
        am.SetFloat("BGMVol", -80);
        gameOver.Play();
        float vol = -80;
        yield return new WaitForSecondsRealtime(gameOver.clip.length * 0.75f);
        for (int i = 0; i < 20; i++) {
            vol /= 1.2f;
            yield return new WaitForSecondsRealtime(0.1f);
            am.SetFloat("BGMVol", vol);
        }
        am.SetFloat("BGMVol", 0);
    }

}
