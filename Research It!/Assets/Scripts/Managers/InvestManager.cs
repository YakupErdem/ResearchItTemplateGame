using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class InvestManager : MonoBehaviour
{
    public float investingTime;
    private string _lastTime, _lastDate;

    private int _passedSeconds;
    private void Start()
    {
        AddMoney();
        FindObjectOfType<UnitsManager>().RefreshTexts();
        StartCoroutine(Invest());
    }

    private void AddMoney()
    {
        _lastDate = SaveSystem.GetString("lastDate");
        _lastTime = SaveSystem.GetString("lastTime");
        CalculateTime();
        Debug.Log("Passed Seconds: " + _passedSeconds);
        decimal addedMoney = _passedSeconds * Convert.ToDecimal(SaveSystem.GetFloat("Invest"));
        Debug.Log("Last Money: " + SaveSystem.GetString("Money"));
        //
        SaveSystem.SetString("Money", (Convert.ToDecimal(SaveSystem.GetString("Money")) + addedMoney).ToString());
        //
        Debug.Log("Added Money: " + SaveSystem.GetString("Money"));
    }

    public void EnhanceInvest(float amount)
    {
        SaveSystem.SetFloat("Invest", SaveSystem.GetFloat("Invest") + amount);
    }
    
    public void ChangeInvest(float amount)
    {
        SaveSystem.SetFloat("Invest", amount);
    }

    private IEnumerator Invest()
    {
        yield return new WaitForSeconds(investingTime);
        SaveSystem.SetString("Money", (Convert.ToDecimal(SaveSystem.GetString("Money")) + Convert.ToDecimal(SaveSystem.GetFloat("Invest"))).ToString());
        FindObjectOfType<UnitsManager>().RefreshTexts();
        StartCoroutine(Invest());
    }

    private void CalculateTime()
    {
        if (_lastDate == "" || _lastTime == "") return;
        string[] currentDate = DateTime.UtcNow.ToString().Split(" ");
        currentDate[1] = DateTime.UtcNow.ToString("HH:mm:ss");
        /*currentDate[0] = "10/9/2022";
        currentDate[1] = "2:30:59";*/
        Debug.Log("Last time: " + _lastTime + " Now: "+ currentDate[1]);
        if (_lastDate != currentDate[0])
        {
            string[] currentDateSplit = currentDate[0].Split("/");
            string[] lastDateSplit = _lastDate.Split("/");
            if (currentDateSplit[0] != lastDateSplit[0])
            {
                int passed = (int.Parse(currentDateSplit[0]) - int.Parse(lastDateSplit[0])) * 2592000;
                _passedSeconds += passed;
            }
            if (currentDateSplit[1] != lastDateSplit[1])
            {
                int passed = (int.Parse(currentDateSplit[1]) - int.Parse(lastDateSplit[1])) * 86400;
                _passedSeconds += passed;
            }
        }
        if (_lastTime != currentDate[1])
        {
            string[] currentDateSplit = currentDate[1].Split(":");
            string[] lastTimeSplit = _lastTime.Split(":");
            Debug.Log(currentDateSplit[0] + " " + lastTimeSplit[0]);
            if (currentDateSplit[0] != lastTimeSplit[0])
            { 
                int passed = (int.Parse(currentDateSplit[0]) - int.Parse(lastTimeSplit[0])) * 3600;
                _passedSeconds += passed;
            }
            if (currentDateSplit[1] != lastTimeSplit[1])
            { 
                int passed = (int.Parse(currentDateSplit[1]) - int.Parse(lastTimeSplit[1])) * 60;
                _passedSeconds += passed;
            }
            if (currentDateSplit[2] != lastTimeSplit[2])
            { 
                int passed = int.Parse(currentDateSplit[2]) - int.Parse(lastTimeSplit[2]);
                _passedSeconds += passed;
            }
        }
    }

    private void OnApplicationQuit()
    {
        string[] date = DateTime.UtcNow.ToString().Split(" ");
        SaveSystem.SetString("lastDate", date[0]);
        SaveSystem.SetString("lastTime", DateTime.UtcNow.ToString("HH:mm:ss"));
    }
}
