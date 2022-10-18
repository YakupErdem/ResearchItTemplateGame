using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchPageLoader : MonoBehaviour
{
    public Image image;
    public Text name;
    public Text desc;
    public Text cost;
    public Text researchCost;
    public Text branch;

    private int _id;
    //
    public void Load(Sprite img, string nm, string description, float cst, float rsCst, ResearchManager.Branch brnch, int id)
    {
        image.sprite = img;
        name.text = nm;
        desc.text = description;
        cost.text = cst.ToString();
        researchCost.text = rsCst.ToString();
        branch.text = brnch.ToString();
        //
        _id = id;
    }

    public void StartResearch()
    {
        FindObjectOfType<ResearchManager>().StartResearch(_id);
    }
}
