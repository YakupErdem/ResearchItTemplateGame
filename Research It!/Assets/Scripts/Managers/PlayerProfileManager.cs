using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public Profile profileThings;
    public ProfilePicture profilePicture;
    [Serializable]
    public struct ProfilePicture
    {
        public Sprite hair;
        public Sprite eye;
        public Sprite nose;
        public Sprite mouth;
    }
    [Serializable]
    public struct Profile
    {
        public Sprite[] hairs;
        public Sprite[] eyes;
        public Sprite[] noses;
        public Sprite[] mouths;
    }

    private void Start()
    {
        
    }

    public void RefreshProfilePicture()
    {
        Set(profilePicture.hair, profileThings.hairs, "PlayerHair");
        Set(profilePicture.eye, profileThings.eyes, "PlayerEye");
        Set(profilePicture.nose, profileThings.noses, "PlayerNose");
        Set(profilePicture.mouth, profileThings.mouths, "PlayerMouth");
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
}