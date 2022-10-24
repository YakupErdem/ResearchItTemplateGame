using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgendaContentManager : MonoBehaviour
{
    public Content[] contents;
    public ContentArgument[] contentsArgument;
    public Transform contentPanel;
    public GameObject contentObject, contentArgumentObject;
    public bool test;
    public bool testArgument;
    public Type testBranch;
    public float refreshCount;

    public static int ContentCount;

    private void Awake()
    {
        ContentCount = 0;
    }

    private void Start()
    {
        RefreshAgendaContents();
    }

    [Serializable]
    public struct Content
    {
        public Sprite contentImage;
        public string by;
        public ContentName contentName;
        public ContentDesc contentDescription;
        public Type type;
    }

    [Serializable]
    public struct ContentArgument
    {
        public string name, name2;
        public ContentDesc contentArgument;
        public Type type;
    }

    [Serializable]
    public enum Type
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

    [Serializable]
    public struct ContentName
    {
        public string eng;
        public string tr;
    }

    [Serializable]
    public struct ContentDesc
    {
        public string eng;
        public string tr;
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            ContentCount++;
            var content = contents[Random.Range(0, contents.Length)];
            var spawnedObject = Instantiate(contentObject, contentPanel);
            FindObjectOfType<ContentPageSizer>().Resize();
            switch (SaveSystem.GetString("Language"))
            {
                case "English":
                    spawnedObject.GetComponent<ContentLoad>().Load(content.contentImage, content.contentName.eng,
                        content.contentDescription.eng, content.by);
                    break;
                case "Turkish":
                    spawnedObject.GetComponent<ContentLoad>().Load(content.contentImage, content.contentName.tr,
                        content.contentDescription.tr, content.by);
                    break;
            }
        }

        if (testArgument)
        {
            testArgument = false;
            var content = contentsArgument[Random.Range(0, contentsArgument.Length)];
            var spawnedObject = Instantiate(contentArgumentObject, contentPanel);
            FindObjectOfType<ContentPageSizer>().Resize();
            switch (SaveSystem.GetString("Language"))
            {
                case "English":
                    spawnedObject.GetComponent<ContentArgumentLoad>()
                        .LoadArgument(content.name, content.name2, content.contentArgument.eng);
                    break;
                case "Turkish":
                    spawnedObject.GetComponent<ContentArgumentLoad>()
                        .LoadArgument(content.name, content.name2, content.contentArgument.tr);
                    break;
            }
        }
    }

    public void RefreshAgendaContents()
    {
        List<Content> sameBranch = new();
        List<Content> nonSameBranch = new();
        List<Content> spawnWithRandomQueue = new();
//
        foreach (var content in contents)
        {
            if (content.type == testBranch)
            {
                sameBranch.Add(content);
            }
            else
            {
                nonSameBranch.Add(content);
            }
        }
//
        for (int i = 0; i < refreshCount; i++)
        {
            if (sameBranch.Count > i)
            {
                T:
                //Debug.Log("Trying To Spawn Agenda With SameBranch");
                int randomNumber = Random.Range(0, sameBranch.Count);
                if (!spawnWithRandomQueue.Contains(sameBranch[randomNumber]))
                {
                    spawnWithRandomQueue.Add(sameBranch[randomNumber]);
                }
                else goto T;
            }
            else
            {
                T:
                //Debug.Log("Trying To Spawn Agenda With NonSameBranch");
                int randomNumber = Random.Range(0, nonSameBranch.Count);
                if (!spawnWithRandomQueue.Contains(nonSameBranch[randomNumber]))
                {
                    spawnWithRandomQueue.Add(nonSameBranch[randomNumber]);
                }
                else goto T;
            }
        }
//
        Spawn(spawnWithRandomQueue);
    }

    private void Spawn(List<Content> contents)
    {
        foreach (var content in contents)
        {
            var spawnedObject = Instantiate(contentObject, contentPanel);
            FindObjectOfType<ContentPageSizer>().Resize();
            switch (SaveSystem.GetString("Language"))
            {
                case "English":
                    spawnedObject.GetComponent<ContentLoad>()
                        .Load(content.contentImage, content.contentName.eng, content.contentDescription.eng, content.by);
                    break;
                case "Turkish":
                    spawnedObject.GetComponent<ContentLoad>()
                        .Load(content.contentImage, content.contentName.tr, content.contentDescription.tr, content.by);
                    break;
            }
        }
    }
}