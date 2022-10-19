using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsManager : MonoBehaviour
{
    public ShopItems[] shopItems;
    public int itemRefreshCount;
    public Transform parentCanvas;
    public GameObject shopSketch;
    public bool reset;

    [Serializable]
    public struct ShopItems
    {
        public Sprite image;
        public Name name;
        public Description description;
        public float cost;
        public Branch branch;
        public int id;
    }
    
    [Serializable]
    public struct Name
    {
        public string eng;
        public string tr;
    }
    [Serializable]
    public struct Description
    {
        public string eng;
        public string tr;
    }
    
    [Serializable]
    public enum Branch
    {
        Space,
        QuantumPhysics,
        Physics,
        Philosophy,
        Math,
        Chemistry,
        Geometry,
        Biology,
        Medicine,
        Evolution,
        Electricity
    }

    private void Start()
    {
        if (reset)
        {
            SaveSystem.SetInt("OwnItemCount", 0);
            for (int i = 0; i < 2; i++)
            {
                SaveSystem.SetInt("Item", 0);
            } 
        }
        _spawnedPages = new Dictionary<int, GameObject>();
        OwnItems = new Dictionary<int, ShopItems>();
        RefreshOwnItems();
        RefreshPages();
        FindObjectOfType<ShopItemRefresher>().RefreshItems();
    }

    private Dictionary<int, GameObject> _spawnedPages;
    public Dictionary<int, ShopItems> OwnItems;

    public void RefreshPages()
    {
        foreach (var shopItem in shopItems)
        {
            if (OwnItems.ContainsKey(shopItem.id))
            {
                continue;
            }
            var spawnedPage = Instantiate(shopSketch, parentCanvas);
            _spawnedPages.Add(shopItem.id, spawnedPage);
            parentCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(
                parentCanvas.GetComponent<RectTransform>().sizeDelta.x,
                parentCanvas.GetComponent<RectTransform>().sizeDelta.y + 463.2646f);
            ShopPageLoader.LoadablePage loadablePage = new ShopPageLoader.LoadablePage();
            loadablePage.image = shopItem.image;
            switch (SaveSystem.GetString("Language"))
            {
                case "Turkish":
                    loadablePage.name = shopItem.name.tr;
                    loadablePage.description = shopItem.description.tr;
                    break;
                case "English":
                    loadablePage.name = shopItem.name.eng;
                    loadablePage.description = shopItem.description.eng;
                    break;
            }
            loadablePage.cost = shopItem.cost;
            loadablePage.branch = shopItem.branch;
            spawnedPage.GetComponent<ShopPageLoader>().Load(loadablePage, shopItem.id);
        }
    }

    public void BuyItem(int id)
    {
        foreach (var shopItem in shopItems)
        {
            if (shopItem.id == id)
            {
                if (Convert.ToDecimal(shopItem.cost) > Convert.ToDecimal(SaveSystem.GetString("Money"))) { return; }
                //
                SaveSystem.SetString("Money",(Convert.ToDecimal(SaveSystem.GetString("Money")) - Convert.ToDecimal(shopItem.cost)).ToString());
            }
        }
        //
        SaveSystem.SetInt("OwnItemCount", SaveSystem.GetInt("OwnItemCount") + 1);
        SaveSystem.SetInt("Item" + SaveSystem.GetInt("OwnItemCount").ToString(), id);
        FindObjectOfType<ShopItemRefresher>().RefreshItems();
        Debug.Log("Succesfully Purchased Item id: " + id);
        Destroy(_spawnedPages[id]);
    }

    public void RefreshOwnItems()
    {
        for (int i = 0; i < SaveSystem.GetInt("OwnItemCount"); i++)
        {
            foreach (var shopItem in shopItems)
            {
                if (shopItem.id == SaveSystem.GetInt("Item" + (i+1)))
                {
                    if (!OwnItems.ContainsKey(shopItem.id))
                    {
                        OwnItems.Add(shopItem.id,shopItem);
                    }
                }
            }
        }
    }
}
