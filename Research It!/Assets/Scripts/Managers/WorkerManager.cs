using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public Worker[] workers;
    public int workerCount;
    public int maxWorkerCount;
    public ArrayList workerList;

    public bool resetWorkerCount;

    private void Awake()
    {
        workerList = new ArrayList();
        if (resetWorkerCount)
        {
            SaveSystem.SetInt("WorkerCount", 0);
            SaveSystem.SetInt("MaxWorkerCount", 3);
        }
        RefreshWorkerStatics();
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
        workerList.Clear();
        maxWorkerCount = SaveSystem.GetInt("MaxWorkerCount");
        workerCount = SaveSystem.GetInt("WorkerCount");
        for (int i = 0; i < workerCount; i++)
        {
            foreach (var worker in workers)
            {
                if (SaveSystem.GetString("Worker" + (i + 1)) == worker.name)
                {
                    workerList.Add(worker);
                    //Debug.Log("Loaded Worker: "+ worker.name);
                }
            }
        }

        WorkerReload.RefreshPage = true;
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
        foreach (Worker worker in workers)
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

    public void SellWorker(string workerName, GameObject g)
    {
        Worker selledWorker = default;
        Debug.Log("Worker count: "+workerList.Count);
        foreach (Worker worker in workerList)
        {
            if (workerName == worker.name)
            {
                Debug.Log("Set selled worker");
                selledWorker = worker;
                break;
            }
        }

        if (workerCount < 2)
        {
            Worker mSelledWorker = (Worker)workerList[0];
            decimal mRefund = (decimal)(mSelledWorker.cost * .7f);
            Destroy(g);
            Debug.Log("Selled Worker : " + mSelledWorker.name + " For: " + mRefund);
            goto Skip;
        }
        Worker lastWorker = default;
        foreach (Worker worker in workerList)
        {
            if (worker.name == SaveSystem.GetString("Worker" + (workerCount - 1)))
            {
                lastWorker = worker;
                break;
            }
        }
        for (int i = 0; i < workerCount; i++)
        {
            Worker worker = (Worker)workerList[i];
            if (worker.name == selledWorker.name)
            {
                workerList[i] = lastWorker;
                workerList[workerCount - 1] = selledWorker;
                break;
            }
        }
        decimal refund = (decimal)(selledWorker.cost * .7f);
        Destroy(g);
        Debug.Log("Selled Worker : " + selledWorker.name + " For: " + refund);
        //SaveSystem.SetString("Money", (Convert.ToDecimal(SaveSystem.GetString("Money") + refund)).ToString());
        Skip:
        SaveSystem.SetInt("WorkerCount", SaveSystem.GetInt("WorkerCount") - 1);
        RefreshWorkerStatics();
    }
}
