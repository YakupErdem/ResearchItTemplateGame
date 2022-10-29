using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RefreshWorkerPage : MonoBehaviour
{
    public Transform parent;
    public GameObject workerPage;
    public Branch testBranch;
    public float workerCount;
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

    private ArrayList workers;

    private void Start()
    {
        workers = new ArrayList();
    }

    public void RefreshWorkers()
    {
        for (int i = 0; i < workerCount; i++)
        {
            WorkerManager.Worker worker = FindObjectOfType<WorkerManager>().workers[Random.Range(0, FindObjectOfType<WorkerManager>().workers.Length)];
            for (int j = 0; j < FindObjectOfType<WorkerManager>().workers.Length; j++)
            {
                if (FindObjectOfType<WorkerManager>().workers[j].branch == (WorkerManager.Branch)testBranch)
                {
                    worker = FindObjectOfType<WorkerManager>().workers[j];
                    if (workers.Contains(worker))
                    {
                        if (j + 1 >= FindObjectOfType<WorkerManager>().workers.Length)
                        {
                            worker = FindObjectOfType<WorkerManager>().workers[Random.Range(0, FindObjectOfType<WorkerManager>().workers.Length)];
                            break;
                        }
                        continue;
                    }
                    workers.Add(worker);
                    break;   
                }
                if (j + 1 >= FindObjectOfType<WorkerManager>().workers.Length)
                {
                    worker = FindObjectOfType<WorkerManager>().workers[Random.Range(0, FindObjectOfType<WorkerManager>().workers.Length)];
                }
            }
            Debug.Log("Spawned Worker = "+ worker.name);
            var spawnedWorkerPage = Instantiate(workerPage, parent);
            spawnedWorkerPage.GetComponent<WorkerPageLoader>().Load(worker.photo, worker.name, worker.cost, worker.moneyPerSecond, worker.researchPointPerSecond, (WorkerPageLoader.Branch)worker.branch);
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(
                parent.GetComponent<RectTransform>().sizeDelta.x,
                parent.GetComponent<RectTransform>().sizeDelta.y + 463.2646f);
        }
    }
}
