using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    public Statics.Items item;
    private GameObject go;
    public Transform spawnPoint;
    private int lastTime = 0;
    public int interval = 1;
    bool hasItem;

    private void Update() {
        if (GameManager.Instance.updateTime - interval > lastTime) {
            if (!hasItem) {
                go = Instantiate(ItemManager.Instance.GetGameObjectFromEnum(item), spawnPoint);
                go.transform.position = spawnPoint.position;
                go.tag = "Item";
                hasItem = true;
            }
            lastTime = GameManager.Instance.updateTime;
        }
    }

    public void TakeItem() {
        Destroy(go);
        hasItem = false;
    }
}
