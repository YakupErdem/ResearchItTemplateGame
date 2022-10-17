using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemRefresher : MonoBehaviour
{
    public Transform parentCanvas;
    public Vector3[] pos;
    public GameObject sketch;
    private void Start()
    {
        spawnedPages = new List<GameObject>();
    }

    public List<GameObject> spawnedPages;

    public void RefreshItems()
    {
        foreach (var ownItem in FindObjectOfType<ShopItemsManager>().OwnItems)
        {
            foreach (var shopItem in FindObjectOfType<ShopItemsManager>().shopItems)
            {
                if (shopItem.id == ownItem.Key)
                {
                    var spawnedObject = Instantiate(sketch, parentCanvas);
                    spawnedObject.transform.localPosition = pos[spawnedPages.Count];
                    spawnedPages.Add(spawnedObject);
                    spawnedObject.GetComponent<ShopItemLoader>().Load(shopItem.image);
                }
            }
        }
    }
}
