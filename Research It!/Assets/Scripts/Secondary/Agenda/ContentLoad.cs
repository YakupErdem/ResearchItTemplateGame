using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentLoad : MonoBehaviour
{
    public Image image, personalImage;
    public Text name;
    public Text description;
    public GameObject biographyPart;
    //
    //
    public static Sprite Sprite;
    public static string Name, Description, by;

    public void Load(Sprite s, string n, string d, string b)
    {
        Sprite = s;
        Name = n;
        Description = d;
        by = b;
        //
        image.sprite = s;
        name.text = n;
        description.text = d;
        LoadProperties();
    }

    private void LoadProperties()
    {
        foreach (var character in FindObjectOfType<SetPeopleTypes>().characters)
        {
            if (character.name == by)
            {
                personalImage.sprite = character.photo;
                break;
            }
        }
    }
}
