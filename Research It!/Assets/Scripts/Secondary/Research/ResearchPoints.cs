using System;
using System.Collections;
using UnityEngine;

public class ResearchPoints : MonoBehaviour
{
    public int researchPointGain;

    private ResearchVariables researchVariables;

    private struct ResearchVariables
    {
        public Sprite image;
        public float researchPoint;
        public WorkerManager.Branch branch;
    }

    private void GetCurrentResearch()
    {
        foreach (var researchPage in FindObjectOfType<ResearchManager>().researchPages)
        {
            if (researchPage.id == SaveSystem.GetInt("CurrentResearch"))
            {
                researchVariables.image = researchPage.image;
                researchVariables.researchPoint = researchPage.researchCost;
                researchVariables.branch = (WorkerManager.Branch)researchPage.branch;
            }
        }
    }
//
    public void RefreshGain()
    {
        researchPointGain = 0;
        researchPointGain += Convert.ToInt16(SaveSystem.GetFloat("ResearchPerSecond"));
        //
        foreach (var workerObject in FindObjectOfType<WorkerManager>().workerList)
        {
            WorkerManager.Worker worker = (WorkerManager.Worker)workerObject;
            if (worker.branch == researchVariables.branch)
            {
                researchPointGain += Convert.ToInt16(worker.researchPointPerSecond);
            }
        }
    }
//
    public void StartResearch()
    {
        GetCurrentResearch();
        RefreshGain();
        StartCoroutine(AddResearchPerSecond());
    }
    //

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator AddResearchPerSecond()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<UnitsManager>().AddResearchPoints(researchPointGain);
        FindObjectOfType<UnitsManager>().RefreshTexts();
        if (SaveSystem.GetFloat("ResearchPoint") >= researchVariables.researchPoint)
        {
            SaveSystem.SetFloat("ResearchPoint", 0);
            SaveSystem.SetInt("CurrentResearch", 0);
            FindObjectOfType<UnitsManager>().RefreshTexts();
            Debug.Log("Research Has Ended");
        }
        else StartCoroutine(AddResearchPerSecond());
    }
}