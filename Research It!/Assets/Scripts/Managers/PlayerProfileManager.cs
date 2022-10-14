using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileManager : MonoBehaviour
{
    public Text nameText;
    public Profile profileThings;
    public ProfilePicture profilePicture;
    public Finance finance;

    public bool setFinancesToOne;
    
    private void Start()
    {
        if (setFinancesToOne) SetFinancesToOne();
        RefreshFinance();
        RefreshNameText();
    }

    private void SetFinancesToOne()
    {
        SaveSystem.SetFloat("MoneyPerClick", 1);
        SaveSystem.SetFloat("ResearchPerSecond", 1);
    }
    
    [Serializable]
    public struct ProfilePicture
    {
        public Sprite hair;
        public Sprite eye;
        public Sprite nose;
        public Sprite mouth;
    }
    [Serializable]
    public struct Finance
    {
        public Text moneyPerClick;
        public Text researchPerSecond;
    }
    [Serializable]
    public struct Profile
    {
        public Sprite[] hairs;
        public Sprite[] eyes;
        public Sprite[] noses;
        public Sprite[] mouths;
    }

    private void RefreshNameText() => nameText.text = SaveSystem.GetString("PlayerName");
    
    public void RefreshProfilePicture()
    {
        Set(profilePicture.hair, profileThings.hairs, "PlayerHair");
        Set(profilePicture.eye, profileThings.eyes, "PlayerEye");
        Set(profilePicture.nose, profileThings.noses, "PlayerNose");
        Set(profilePicture.mouth, profileThings.mouths, "PlayerMouth");
    }

    private void RefreshFinance()
    {
        finance.moneyPerClick.text = SaveSystem.GetFloat("MoneyPerClick").ToString();
        finance.researchPerSecond.text = SaveSystem.GetFloat("ResearchPerSecond").ToString();
    }

    private void Set(Sprite mainSprite, Sprite[] s, string savePath)
    {
        foreach (var sprite in s)
        {
            if (sprite.name == SaveSystem.GetString(savePath))
            {
                Debug.Log("Set hair to: "+ sprite.name);
                mainSprite = sprite;
            }
        }
    }

    public void UpdateMoneyPerClick(float amount)
    {
        Debug.Log("Updated MoneyPerClick");
        //
        SaveSystem.SetFloat("MoneyPerClick", SaveSystem.GetFloat("MoneyPerClick") + amount);
        RefreshFinance();
    }
    
    public void UpdateResearchPerSecond(float amount)
    {
        Debug.Log("Updated ResearchPerSecond");
        //
        SaveSystem.SetFloat("ResearchPerSecond", SaveSystem.GetFloat("ResearchPerSecond") + amount);
        RefreshFinance();
    }
}