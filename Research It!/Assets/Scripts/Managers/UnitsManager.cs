using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    public Text moneyText, researchText, investText;

    private void Start()
    {
        RefreshTexts();
    }

    public void AddMoney(float amount)
    {
        SaveSystem.SetString("Money", (Convert.ToDecimal(SaveSystem.GetString("Money")) + Convert.ToDecimal(amount)).ToString());
        RefreshTexts();
    }

    public void Click()
    {
        SaveSystem.SetString("Money", (Convert.ToDecimal(SaveSystem.GetString("Money")) + Convert.ToDecimal(SaveSystem.GetFloat("MoneyPerClick"))).ToString());
        RefreshTexts();
    }
    

    public void AddResearchPoints(float amount)
    {
        SaveSystem.SetFloat("ResearchPoint", SaveSystem.GetFloat("ResearchPoint") + amount);
        RefreshTexts();
    }
    
    public void RefreshTexts()
    {
        moneyText.text = FindObjectOfType<CurrentMoney>().ConvertMoney(SaveSystem.GetString("Money")).ToString();
        researchText.text = SaveSystem.GetFloat("ResearchPoint").ToString();
        investText.text = SaveSystem.GetFloat("Invest").ToString() + "/s";
    }
}
