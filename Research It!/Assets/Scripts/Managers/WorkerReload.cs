using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerReload : MonoBehaviour
{
    public GameObject workerPage, parent;
    private void Start()
    {
        ArrayList currentWorkers = new ArrayList();
        for (int i = 0; i < FindObjectOfType<WorkerManager>().workerCount; i++)
        {
            string workerName = SaveSystem.GetString("Worker" + (i + 1));
            foreach (var worker in FindObjectOfType<WorkerManager>().workers)
            {
                if (worker.name == workerName)
                {
                    currentWorkers.Add(worker);
                    break;
                }
            }
        }
        foreach (WorkerManager.Worker worker in currentWorkers)
        {
            var page =Instantiate(workerPage, parent.transform);
            page.GetComponent<WorkerInfoLoader>().Load(worker.photo, worker.name, worker.cost, worker.moneyPerSecond,
                worker.researchPointPerSecond, (WorkerInfoLoader.Branch)worker.branch);
        }
    }
}
