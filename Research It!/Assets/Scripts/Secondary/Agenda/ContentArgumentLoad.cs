using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentArgumentLoad : MonoBehaviour
{
    public Image image1, image2;
    public Text name, name2;
    public Text argument;
    public GameObject biographyPart;
    //
    //
    public static string Name1, Name2, Argument;

    public void LoadArgument(string nm1, string nm2, string argmnt)
    {
        Name1 = nm1;
        Name2 = nm2;
        Argument = argmnt;
        //
        name.text = nm1;
        name2.text = nm2;
        argument.text = argmnt;
        LoadProperties();
    }

    private void LoadProperties()
    {
        foreach (var character in FindObjectOfType<SetPeopleTypes>().characters)
        {
            if (character.name == Name1)
            {
                image1.sprite = character.photo;
            }
            if (character.name == Name2)
            {
                image2.sprite = character.photo;
            }
        }
    }
}
