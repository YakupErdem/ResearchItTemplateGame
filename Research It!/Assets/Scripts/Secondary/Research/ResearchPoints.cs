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
            EndResearch();
        }
        else StartCoroutine(AddResearchPerSecond());
    }

    private void EndResearch()
    {
        SaveSystem.SetFloat("ResearchPoint", 0);
        MakeMagazine();
        SaveSystem.SetInt("CurrentResearch", 0);
        FindObjectOfType<UnitsManager>().RefreshTexts();
        Debug.Log("Research Has Ended");
        //
    }

    private void MakeMagazine()
    {
        ResearchManager.ResearchMagazine selectedMagazine = default;
        foreach (var researchMagazine in FindObjectOfType<ResearchManager>().researchMagazines)
        {
            if (researchMagazine.id == SaveSystem.GetInt("CurrentResearch"))
            {
                selectedMagazine = researchMagazine;
                Debug.Log("SelectedMagazine" + selectedMagazine.Name.eng);
            }
        }

        //
        Transform parent = FindObjectOfType<ResearchManager>().parentMagazineCanvas;
        var spawnedMagazine = Instantiate(FindObjectOfType<ResearchManager>().magazineSketch,
            parent);
        switch (SaveSystem.GetString("Language"))
        {
            case "Turkish":
                spawnedMagazine.GetComponent<PlayerResearchLoader>().Load(selectedMagazine.Name.tr,
                    ReWriteMagazineText(selectedMagazine.description.tr), selectedMagazine.image);
                break;
            case "English":
                spawnedMagazine.GetComponent<PlayerResearchLoader>().Load(selectedMagazine.Name.eng,
                    ReWriteMagazineText(selectedMagazine.description.eng), selectedMagazine.image);
                break;
        }
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(
            parent.GetComponent<RectTransform>().sizeDelta.x,
            parent.GetComponent<RectTransform>().sizeDelta.y + 463.2646f);
    }

    private string ReWriteMagazineText(string sentence)
    {
        Debug.Log("Magazine sentence: "+ sentence);
        string[] words = sentence.Split(" ");
        string newSentence = String.Empty;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i] == "|name|")
            {
                words[i] = SaveSystem.GetString("PlayerName");
            }
            newSentence += words[i] + " ";
        }
//
        return newSentence;
    }
}