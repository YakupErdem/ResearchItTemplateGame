using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerReload : MonoBehaviour
{
    public GameObject workerPage, parent;
    public static bool RefreshPage;
    public static List<GameObject> infos;

    private void Awake()
    {
        infos = new List<GameObject>();
        RefreshPage = false;
        Refresh();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Refresh()
    {
        infos.Clear();
        ArrayList currentWorkers = new ArrayList();
        for (int i = 0; i < FindObjectOfType<WorkerManager>().workerCount; i++)
        {
            string workerName = SaveSystem.GetString("Worker" + (i + 1));
            Debug.Log(workerName);
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
            var page = Instantiate(workerPage, parent.transform);
            infos.Add(page);
            page.GetComponent<WorkerInfoLoader>().Load(worker.photo, worker.name, worker.cost, worker.moneyPerSecond,
                worker.researchPointPerSecond, (WorkerInfoLoader.Branch)worker.branch);
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(
                parent.GetComponent<RectTransform>().sizeDelta.x,
                parent.GetComponent<RectTransform>().sizeDelta.y + 463.2646f);
        }
    }
}
