using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerInfoLoader : MonoBehaviour
{
    public Image image;
    public Text name;
    public Text cost;
    public Text moneyPerSecond;
    public Text researchPerSecond;
    public Text branchText;
    public Branch branch;
    
    [Serializable]
    public enum Branch
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

    public void Load(Sprite img, string nm, float cst, float mnyprscnd, float rsrchprscnd, Branch brnch)
    {
        image.sprite = img;
        name.text = nm;
        cost.text = cst.ToString();
        branch = brnch;
        branchText.text = brnch.ToString();
        if (SaveSystem.GetString("Language") == "Turkish")
        {
            moneyPerSecond.text = mnyprscnd.ToString() + "/Saniye başı para";
            researchPerSecond.text = rsrchprscnd.ToString() + "/Saniye başı araştırma";
        }
        else
        {
            moneyPerSecond.text = mnyprscnd.ToString() + "/Money per second";
            researchPerSecond.text = rsrchprscnd.ToString() + "/Research Point per second";
        }
    }
}
