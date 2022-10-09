using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetClickerActive : MonoBehaviour
{
    private GameObject _clickArea;

    private void Start() => _clickArea = GetComponentInChildren<Button>().gameObject;
    public void Set(bool b) => _clickArea.SetActive(b);
}
