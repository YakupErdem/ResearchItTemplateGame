using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerProfileManager : MonoBehaviour
{
    public Text nameText;
    public Profile profileThings;
    public ProfilePicture profilePicture;
    public Finance finance;
    public BodyGameObjects bodyGameObjects;
    public bool setFinancesToOne;
    public bool setBodyPartsToOne;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (setFinancesToOne) SetFinancesToOne();
            RefreshFinance();
            RefreshNameText();
            return;
        }
        //
        if (setBodyPartsToOne)
        {
            SaveSystem.SetFloat("Hair", 0);
            SaveSystem.SetFloat("Eye", 0);
            SaveSystem.SetFloat("Nose", 0);
            SaveSystem.SetFloat("Mouth", 0);
        }
        RefreshProfilePicture();
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
    
    [Serializable]
    public struct BodyGameObjects
    {
        public GameObject hair;
        public GameObject eye;
        public GameObject nose;
        public GameObject mouth;
    }

    private void LoadBodyPart()
    {
        bodyGameObjects.hair.GetComponent<BodyPartLoader>().Load(profilePicture.hair);
        bodyGameObjects.eye.GetComponent<BodyPartLoader>().Load(profilePicture.eye);
        bodyGameObjects.nose.GetComponent<BodyPartLoader>().Load(profilePicture.nose);
        bodyGameObjects.mouth.GetComponent<BodyPartLoader>().Load(profilePicture.mouth);
    }

    private void RefreshNameText() => nameText.text = SaveSystem.GetString("PlayerName");

    public void RefreshProfilePicture()
    {
        profileNumbers.hair = SaveSystem.GetFloat("Hair");
        profileNumbers.eye = SaveSystem.GetFloat("Eye");
        profileNumbers.nose = SaveSystem.GetFloat("Nose");
        profileNumbers.mouth = SaveSystem.GetFloat("Mouth");
        //
        profilePicture.hair = profileThings.hairs[(int)profileNumbers.hair];
        profilePicture.eye = profileThings.eyes[(int)profileNumbers.eye];
        profilePicture.nose = profileThings.noses[(int)profileNumbers.nose];
        profilePicture.mouth = profileThings.mouths[(int)profileNumbers.mouth];
        LoadBodyPart();
    }

    private void RefreshFinance()
    {
        finance.moneyPerClick.text = SaveSystem.GetFloat("MoneyPerClick").ToString();
        finance.researchPerSecond.text = SaveSystem.GetFloat("ResearchPerSecond").ToString();
    }

    private ProfileNumbers profileNumbers;
    private struct ProfileNumbers
    {
        public float hair;
        public float eye;
        public float nose;
        public float mouth;
    }
    public void BodyPartRight(string part)
    {
        switch (part)
        {
            case "hair":
                SaveSystem.SetFloat("Hair", profileNumbers.hair + 1);
                if (SaveSystem.GetFloat("Hair") >= profileThings.hairs.Length)
                {
                    SaveSystem.SetFloat("Hair", 0);
                }
                break;
            case "eye":
                SaveSystem.SetFloat("Eye", profileNumbers.eye + 1);
                if (SaveSystem.GetFloat("Eye") >= profileThings.eyes.Length)
                {
                    SaveSystem.SetFloat("Eye", 0);
                }
                break;
            case "nose":
                SaveSystem.SetFloat("Nose", profileNumbers.nose + 1);
                if (SaveSystem.GetFloat("Nose") >= profileThings.noses.Length)
                {
                    SaveSystem.SetFloat("Nose", 0);
                }
                break;
            case "mouth":
                SaveSystem.SetFloat("Mouth", profileNumbers.mouth + 1);
                if (SaveSystem.GetFloat("Mouth") >= profileThings.mouths.Length)
                {
                    SaveSystem.SetFloat("Mouth", 0);
                }
                break;
        }
        RefreshProfilePicture();
    }
    
    public void BodyPartLeft(string part)
    {
        switch (part)
        {
            case "hair":
                SaveSystem.SetFloat("Hair", profileNumbers.hair - 1);
                if (SaveSystem.GetFloat("Hair") < 0)
                {
                    SaveSystem.SetFloat("Hair", profileThings.hairs.Length - 1);
                }
                break;
            case "eye":
                SaveSystem.SetFloat("Eye", profileNumbers.eye - 1);
                if (SaveSystem.GetFloat("Eye") < 0)
                {
                    SaveSystem.SetFloat("Eye", profileThings.eyes.Length - 1);
                }
                break;
            case "nose":
                SaveSystem.SetFloat("Nose", profileNumbers.nose - 1);
                if (SaveSystem.GetFloat("Nose") < 0)
                {
                    SaveSystem.SetFloat("Nose", profileThings.noses.Length - 1);
                }
                break;
            case "mouth":
                SaveSystem.SetFloat("Mouth", profileNumbers.mouth - 1);
                if (SaveSystem.GetFloat("Mouth") < 0)
                {
                    SaveSystem.SetFloat("Mouth", profileThings.mouths.Length - 1);
                }
                break;
        }
        RefreshProfilePicture();
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