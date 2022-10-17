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
        _spawnedPages = new Dictionary<int, GameObject>();
        _ownItems = new Dictionary<int, ShopItems>();
        RefreshPages();
    }

    private Dictionary<int, GameObject> _spawnedPages;
    private Dictionary<int, ShopItems> _ownItems;

    public void RefreshPages()
    {
        foreach (var shopItem in shopItems)
        {
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
                    _ownItems.Add(shopItem.id,shopItem);
                }
            }
        }
    }
}
