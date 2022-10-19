using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResearchLoader : MonoBehaviour
{
    public Image image;
    public Text name;
    public Text description;
    public Image researchImage;


    public void Load( /*Image img ,*/ string nm, string desc, Sprite researchImg)
    {
        //image.sprite = img;
        name.text = nm;
        description.text = desc;
        researchImage.sprite = researchImg;
    }
}