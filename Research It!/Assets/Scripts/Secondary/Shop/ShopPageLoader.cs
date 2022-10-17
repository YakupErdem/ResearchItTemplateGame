using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageLoader : MonoBehaviour
{
    public Page page;
    
    [Serializable]
    public struct Page
    {
        public Image image;
        public Text name;
        public Text description;
        public Text cost;
        public Text branch;
    }
    [Serializable]
    public struct LoadablePage
    {
        public Sprite image;
        public string name;
        public string description;
        public float cost;
        public ShopItemsManager.Branch branch;
    }

    private int _id;
    
    public void Load(LoadablePage loadPage, int id)
    {
        _id = id;
        page.image.sprite = loadPage.image;
        page.name.text = loadPage.name;
        page.description.text = loadPage.description;
        page.cost.text = loadPage.cost.ToString();
        page.branch.text = loadPage.branch.ToString();
    }

    public void Buy()
    {
        FindObjectOfType<ShopItemsManager>().BuyItem(_id);
    }
}
