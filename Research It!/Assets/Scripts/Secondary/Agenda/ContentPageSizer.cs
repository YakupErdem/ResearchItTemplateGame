using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPageSizer : MonoBehaviour
{
    public float sizeMultiplier;

    public void Resize()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x,
            GetComponent<RectTransform>().sizeDelta.y + 390.8546f + sizeMultiplier);
    }
}