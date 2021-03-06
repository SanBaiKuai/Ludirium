﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public static ItemManager Instance { get; private set; }
    public GameObject gear;
    public GameObject spring;
    public GameObject screw;
    public GameObject oil;
    public GameObject gearNoBox;
    public GameObject springNoBox;
    public GameObject screwNoBox;

    private void Awake() {
        Instance = this;
    }

    public GameObject GetGameObjectFromEnum(Statics.Items item) {
        switch (item) {
            case Statics.Items.GEAR:
                return gear;
            case Statics.Items.SPRING:
                return spring;
            case Statics.Items.SCREW:
                return screw;
            case Statics.Items.OIL:
                return oil;
            default:
                return null;
        }
    }

    public GameObject GetNoBoxObjectFromEnum(Statics.Items item) {
        switch (item) {
            case Statics.Items.GEAR:
                return gearNoBox;
            case Statics.Items.SPRING:
                return springNoBox;
            case Statics.Items.SCREW:
                return screwNoBox;
            default:
                return null;
        }
    }
}
