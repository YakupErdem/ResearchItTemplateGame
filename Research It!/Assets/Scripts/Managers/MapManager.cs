using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    public Map[] maps;
    public Image backgroundImage;
    
    [Serializable]
    public struct Map
    {
        public string name;
        public Sprite background;
        public UnityEvent mapEvent;
    }

    private void Start()
    {
        if (IsItContains())
        {
            RefreshMap();
        }
        else
        {
            SaveSystem.SetString("Map", maps[0].name);
            RefreshMap();
        }
    }

    public bool test;
    public string testMap;
    private void Update()
    {
        if (test)
        {
            test = false;
            ChangeMap(testMap);
        }
    }


    public void ChangeMap(string mapName)
    {
        SaveSystem.SetString("Map", mapName);
        RefreshMap();
    }

    public bool IsItContains()
    {
        foreach (var map in maps)
        {
            if (map.name == SaveSystem.GetString("Map"))
            {
                return true;
            }
        }
        return false;
    }
    
    public void RefreshMap()
    {
        foreach (var map in maps)
        {
            if (map.name == SaveSystem.GetString("Map"))
            {
                ChangeMap(map);
            }
        }
    }

    private void ChangeMap(Map map)
    {
        map.mapEvent.Invoke();
        backgroundImage.sprite = map.background;
    }
}