using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLogin : MonoBehaviour
{
    public LoginParts loginParts;

    [Serializable]
    public struct LoginParts
    {
        public InputField inputField;
    }
    
    public void Login()
    {
        string name = loginParts.inputField.text;
        SaveSystem.SetString("PlayerName", name);
        SceneManager.LoadScene("SampleScene");
    }
}
