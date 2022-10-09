using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMoney : MonoBehaviour
{
    public static decimal Money;

    private void Awake()
    {
        Money = Convert.ToDecimal(SaveSystem.GetString("Money"));
    }

    public decimal ConvertMoney(string a)
    {
        return Convert.ToDecimal(a);
    }
}
