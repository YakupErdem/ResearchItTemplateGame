using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangaugeTextSet : MonoBehaviour
{
    private Text _text;
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        _text.text = SaveSystem.GetString("Language");
    }
}
