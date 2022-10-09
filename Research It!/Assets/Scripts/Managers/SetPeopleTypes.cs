using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetPeopleTypes : MonoBehaviour
{
    public Character[] characters;

    [Serializable]
    public struct Character
    {
        public Sprite photo;
        public string name;
        public Description description;
        public Branch[] branches;
    }
    
    [Serializable]
    public struct Description
    {
        public string english;
        public string turkish;
    }
    
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
}

