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

    public bool resetWorkerCount;
    
    private void Start()
    {
        workerList = new ArrayList();
        if (resetWorkerCount)
        {
            SaveSystem.SetInt("WorkerCount", 0);
            SaveSystem.SetInt("MaxWorkerCount", 3);
        }
        RefreshWorkerStatics();
        for (int i = 0; i < workerCount; i++)
        {
            foreach (var worker in workers)
            {
                if (SaveSystem.GetString("Worker" + (i + 1)) == worker.name)
                {
                    workerList.Add(worker);
                    Debug.Log("Loaded Worker: "+ worker.name);
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
                    SaveSystem.SetString("Worker" + SaveSystem.GetInt("WorkerCount"), worker.name);
                    RefreshWorkerStatics();
                }
            }
        }
    }
    
    public void BuyWorker(string workerName, GameObject buyedObject)
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
                    Destroy(buyedObject);
                    SaveSystem.SetInt("WorkerCount", SaveSystem.GetInt("WorkerCount") + 1);
                    SaveSystem.SetString("Worker" + SaveSystem.GetInt("WorkerCount"), worker.name);
                    RefreshWorkerStatics();
                }
            }
        }
    }
}
