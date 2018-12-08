﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    private Image image;
    public GameObject[] pages;
    public Button prev;
    public Button next;
    public Button skipButton;
    int currPageNum = 0;

    private void Start() {
        image = GetComponent<Image>();
        StartTutorial();
    }

    public void StartTutorial() {
        Time.timeScale = 0;
        image.enabled = true;
        pages[0].SetActive(true);
        prev.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        prev.interactable = false;
    }

    public void GoNext() {
        pages[currPageNum].SetActive(false);
        currPageNum++;
        pages[currPageNum].SetActive(true);
        if (currPageNum == pages.Length - 1) {
            next.interactable = false;
        }
    }

    public void GoPrev() {
        pages[currPageNum].SetActive(false);
        currPageNum--;
        pages[currPageNum].SetActive(true);
        if (currPageNum == 0) {
            prev.interactable = false;
        }
    }

    public void Skip() {
        Time.timeScale = 1;
        image.enabled = false;
        pages[currPageNum].SetActive(false);
        prev.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
    }
}
