using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public Worker[] workers;
    public int workerCount;
    public int maxWorkerCount;
    public ArrayList workerList;

    private void Start()
    {
        RefreshWorkerStatics();
        for (int i = 0; i < workerCount; i++)
        {
            foreach (var worker in workers)
            {
                if (SaveSystem.GetString("Worker" + (i + 1)) == worker.name)
                {
                    workerList.Add(worker);
                }
            }
        }
    }

    [Serializable]
    public struct Worker
    {
        public Sprite photo;
        public string name;
        public float researchPointPerSecond, moneyPerSecond, cost;
        public Branch branch;
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
    public void RefreshWorkerStatics()
    {
        maxWorkerCount = SaveSystem.GetInt("MaxWorkerCount");
        workerCount = SaveSystem.GetInt("WorkerCount");
    }

    public void BuyWorker(string workerName)
    {
        if (workerCount >= maxWorkerCount)
        {
            Debug.Log("Cant Buy More Workers");
            return;
        }
        foreach (var worker in workers)
        {
            if (worker.name == workerName)
            {
                if (Convert.ToDecimal(SaveSystem.GetString("Money")) >= Convert.ToDecimal(worker.cost))
                {
                    Debug.Log("Successfully purchased worker " + workerName);
                    SaveSystem.SetInt("WorkerCount", SaveSystem.GetInt("WorkerCount") + 1);
                    RefreshWorkerStatics();
                }
            }
        }
    }
}
