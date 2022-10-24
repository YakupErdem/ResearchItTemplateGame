using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPageSizer : MonoBehaviour
{
    public GameObject main;

    public void Resize()
    {
        main.GetComponent<RectTransform>().sizeDelta = new Vector2(main.GetComponent<RectTransform>().sizeDelta.x,
            main.GetComponent<RectTransform>().sizeDelta.y + 519.2646f);
    }
}