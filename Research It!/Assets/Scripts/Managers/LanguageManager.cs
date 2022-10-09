using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    private string[] _languageTypes = { "Turkish", "English" };
    
    public Language[] manageLanguages;

    [Serializable]
    public struct Language
    {
        public Text text;
        public string eng;
        public string tr;
    }

    private void Start()
    {
        if (!_languageTypes.Contains(SaveSystem.GetString("Language")))
        {
            SaveSystem.SetString("Language", "English");
        }
        RefreshLanguage();
    }

    private void RefreshLanguage()
    {
        foreach (var language in manageLanguages)
        {
            switch (SaveSystem.GetString("Language"))
            {
                case "Turkish":
                    language.text.text = language.tr;
                    break;
                case "English":
                    language.text.text = language.eng;
                    break;
            } 
        }
    }

    public void ChangeLanguage(string l)
    {
        if (_languageTypes.Contains(l))
        {
            SaveSystem.SetString("Language", l);
            return;
        }
        Debug.Log("Language type \"" + l + "\" is not supported.");
    }
    
    public void ChangeLanguageByNext(Text text = null)
    {
        int currentLanguage = SaveSystem.GetString("Language") switch
        {
            "English" => 0,
            "Turkish" => 1,
            _ => 0
        };
        if (currentLanguage >= _languageTypes.Length)
        {
            currentLanguage = 0;
        }
        SaveSystem.SetString("Language", _languageTypes[currentLanguage]);
        if (text != null)
        {
            text.text = _languageTypes[currentLanguage];
        }
        RefreshLanguage();
    }
}
