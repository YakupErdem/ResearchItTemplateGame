using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResearchManager : MonoBehaviour
{
    public ResearchPages[] researchPages;

    //
    public GameObject researchSketch;

    public Transform parentCanvas;

    //
    public float researchPageCount;

    public Branch testBranch;

    //
    public static int CurrentResearch;
    public static int CurrentResearchCost;

    [Serializable]
    public struct ResearchPages
    {
        public Sprite image;
        public Name name;
        public Description description;
        public Branch branch;
        public float moneyCost;
        public float researchCost;
        public int id;
    }

    [Serializable]
    public struct Name
    {
        public string eng;
        public string tr;
    }

    [Serializable]
    public struct Description
    {
        public string eng;
        public string tr;
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

    public enum SimilarWithSpace
    {
        QuantumPhysics,
        Physics
    }

    public enum SimilarWithQuantumPhysics
    {
        Physics,
        Electricity,
        Math
    }

    public enum SimilarWithPhysics
    {
        QuantumPhysics,
        Math,
        Space
    }

    public enum SimilarWithPhilosophy
    {
        Space,
        Evolution
    }

    public enum SimilarWithMath
    {
        Geometry,
        Physics
    }

    public enum SimilarWithChemistry
    {
        Physics,
        Medicine,
        Biology
    }

    public enum SimilarWithGeometry
    {
        Math
    }

    public enum SimilarWithBiology
    {
        Evolution,
        Medicine,
        Chemistry
    }

    public enum SimilarWithMedicine
    {
        Biology,
        Chemistry
    }

    public enum SimilarWithEvolution
    {
        Biology
    }

    public enum SimilarWithElectricity
    {
        Physics,
        QuantumPhysics
    }

    private struct CountOfBranches
    {
        public float Space,
            QuantumPhysics,
            Physics,
            Philosophy,
            Math,
            Chemistry,
            Geometry,
            Biology,
            Medicine,
            Evolution,
            Electricity;
    }

    private struct SimilarBranches
    {
        public List<Branch> Space,
            QuantumPhysics,
            Physics,
            Philosophy,
            Math,
            Chemistry,
            Geometry,
            Biology,
            Medicine,
            Evolution,
            Electricity;
    }

    private CountOfBranches countOfBranches;
    private SimilarBranches similarBranches;

    private void Start()
    {
        CheckIfHaveResearch();
        SetSimilarOnes();
        GetNumberOfBranches();
        RefreshResearchPage();
        SpawnPages();
    }
    //

    private void CheckIfHaveResearch()
    {
        foreach (var researchPage in researchPages)
        {
            if (researchPage.id == SaveSystem.GetInt("CurrentResearch"))
            {
                Debug.Log("CurrentResearch = " + researchPage.id);
                CurrentResearch = researchPage.id;
                CurrentResearchCost = Convert.ToInt16(researchPage.researchCost);
                FindObjectOfType<ResearchPoints>().StartResearch();
            }
        }
        //
    }

    //
    private void SetSimilarOnes()
    {
        similarBranches.Space = new List<Branch>
            { Branch.QuantumPhysics, Branch.Physics, Branch.Electricity, Branch.Geometry };
        similarBranches.QuantumPhysics = new List<Branch> { Branch.Electricity, Branch.Physics, Branch.Math };
        similarBranches.Physics = new List<Branch> { Branch.QuantumPhysics, Branch.Math, Branch.Space };
        similarBranches.Philosophy = new List<Branch> { Branch.Space, Branch.Evolution };
        similarBranches.Math = new List<Branch> { Branch.Geometry, Branch.Physics, Branch.QuantumPhysics };
        similarBranches.Chemistry = new List<Branch> { Branch.Medicine, Branch.Physics, Branch.Biology };
        similarBranches.Geometry = new List<Branch> { Branch.Math };
        similarBranches.Biology = new List<Branch> { Branch.Evolution, Branch.Medicine, Branch.Chemistry };
        similarBranches.Medicine = new List<Branch> { Branch.Biology, Branch.Chemistry };
        similarBranches.Evolution = new List<Branch> { Branch.Biology };
        similarBranches.Electricity = new List<Branch> { Branch.QuantumPhysics, Branch.Physics, Branch.Space };
    }

    //
    private List<ResearchPages> addedResearchPages = new();


    private void GetNumberOfBranches()
    {
        foreach (var researchPage in researchPages)
        {
            Debug.Log("Added " + researchPage.branch + " 1");
            switch (researchPage.branch)
            {
                case Branch.Space:
                    countOfBranches.Space++;
                    break;
                case Branch.QuantumPhysics:
                    countOfBranches.QuantumPhysics++;
                    break;
                case Branch.Physics:
                    countOfBranches.Physics++;
                    break;
                case Branch.Philosophy:
                    countOfBranches.Philosophy++;
                    break;
                case Branch.Math:
                    countOfBranches.Math++;
                    break;
                case Branch.Chemistry:
                    countOfBranches.Chemistry++;
                    break;
                case Branch.Geometry:
                    countOfBranches.Geometry++;
                    break;
                case Branch.Biology:
                    countOfBranches.Biology++;
                    break;
                case Branch.Medicine:
                    countOfBranches.Medicine++;
                    break;
                case Branch.Evolution:
                    countOfBranches.Evolution++;
                    break;
                case Branch.Electricity:
                    countOfBranches.Electricity++;
                    break;
            }
        }
    }

    private int currentTry;

    private void RefreshResearchPage()
    {
        for (int i = 0; i < researchPageCount; i++)
        {
            currentTry = i;
            if (addedResearchPages.Contains(researchPages[i]))
            {
                Debug.Log("Retried cause it already contains");
                retriedCount--;
                continue;
            }

            //
            /*if (retriedCount + 1 >= researchPageCount)
            {
                if (!(addedResearchPages.Count >= researchPageCount))
                {
                    i--;
                }
            }*/
            //
            Debug.Log("Tried " + (i + 1) + " Time(s)");
//             //
            //
            if (researchPages[i].branch != testBranch)
            {
                LookSimilarOnes();
            }
            else Spawn(researchPages[i].branch);
        }
    }

    private int retriedCount;

    private void Spawn(Branch branch)
    {
        if (addedResearchPages.Count >= researchPageCount)
        {
            Debug.Log("ReachedMax");
            return;
        }

        retriedCount++;
        //
        foreach (var researchPage in researchPages)
        {
            if (addedResearchPages.Contains(researchPage))
            {
                continue;
            }

            if (researchPage.branch == branch)
            {
                Debug.Log("Spawned " + researchPage.branch);
                addedResearchPages.Add(researchPage);
            }
        }
    }

    private void Spawn(Branch branch, bool skip)
    {
        if (!skip)
        {
            Spawn(branch);
            return;
        }

        retriedCount++;
        foreach (var researchPage in researchPages)
        {
            if (addedResearchPages.Contains(researchPage))
            {
                continue;
            }

            if (researchPage.branch == branch)
            {
                Debug.Log("Spawned " + researchPage.branch);
                addedResearchPages.Add(researchPage);
            }
        }
    }


    private int similarCount;

    private void LookSimilarOnes()
    {
        Debug.Log("Looking to similar ones " + similarCount);
        //
        switch (testBranch)
        {
            case Branch.Space:
                if (similarBranches.Space.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.QuantumPhysics:
                if (similarBranches.QuantumPhysics.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Physics:
                if (similarBranches.Physics.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                /*
                Spawn(similarBranches.Physics[Random.Range(0, similarBranches.Physics.Count)]);
                if (similarCount >= similarBranches.Physics.Count) similarCount = 0;
                Spawn(similarBranches.Physics[similarCount], true);*/
                break;
            case Branch.Philosophy:
                if (similarBranches.Philosophy.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Math:
                if (similarBranches.Math.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Chemistry:
                if (similarBranches.Chemistry.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Geometry:
                if (similarBranches.Geometry.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Biology:
                if (similarBranches.Biology.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Medicine:
                if (similarBranches.Medicine.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Evolution:
                if (similarBranches.Evolution.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
            case Branch.Electricity:
                if (similarBranches.Electricity.Contains(researchPages[currentTry].branch))
                {
                    Spawn(researchPages[currentTry].branch, true);
                }

                break;
        }

        similarCount++;
    }

    private Dictionary<int, GameObject> spawnedPages;

    private void SpawnPages()
    {
        List<ResearchPages> sameBranches = new List<ResearchPages>();

        ResearchPages currentResearch = default;
        foreach (var researchPage in addedResearchPages)
        {
            if (researchPage.id == CurrentResearch)
            {
                currentResearch = researchPage;
            }
        }
        addedResearchPages.Remove(currentResearch);
        //
        foreach (var researchPage in addedResearchPages)
        {
            if (researchPage.branch == testBranch)
            {
                sameBranches.Add(researchPage);
            }
        }

        //
        if (sameBranches.Count > 0)
        {
            foreach (var sameBranch in sameBranches)
            {
                SpawnPage(sameBranch);
            }
        }

        //
        foreach (var researchPage in addedResearchPages)
        {
            if (!sameBranches.Contains(researchPage))
            {
                SpawnPage(researchPage);
            }
        }
    }

    public Dictionary<int, GameObject> spawnedObjects = new();

    private void SpawnPage(ResearchPages researchPage)
    {
        var spawnedPage = Instantiate(researchSketch, parentCanvas);
        spawnedObjects.Add(researchPage.id, spawnedPage);
        parentCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(
            parentCanvas.GetComponent<RectTransform>().sizeDelta.x,
            parentCanvas.GetComponent<RectTransform>().sizeDelta.y + 463.2646f);
        switch (SaveSystem.GetString("Language"))
        {
            case "Turkish":
                spawnedPage.GetComponent<ResearchPageLoader>().Load(researchPage.image,
                    researchPage.name.tr, researchPage.description.tr,
                    researchPage.moneyCost, researchPage.researchCost,
                    researchPage.branch, researchPage.id);
                break;
            case "English":
                spawnedPage.GetComponent<ResearchPageLoader>().Load(researchPage.image,
                    researchPage.name.eng, researchPage.description.eng,
                    researchPage.moneyCost, researchPage.researchCost,
                    researchPage.branch, researchPage.id);
                break;
        }
    }

    public void StartResearch(int id)
    {
        ResearchPages startedResearch = default;
        //
        foreach (var researchPage in researchPages)
        {
            if (researchPage.id == id)
            {
                startedResearch = researchPage;
            }
        }
        //
        if (Convert.ToDecimal(SaveSystem.GetString("Money")) < Convert.ToDecimal(startedResearch.moneyCost))
        {
              Debug.Log("Not Enough Money For Researching");
              return;
        }
        //
        SaveSystem.SetInt("CurrentResearch", id);
        Destroy(spawnedObjects[id]);
        FindObjectOfType<ResearchPoints>().StartResearch();
        Debug.Log("Started Research ID: " + id);
    }
}
/*switch (branch)
        {
            case Branch.Space:
                    if (countOfBranches.Space <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.QuantumPhysics:
                    if (countOfBranches.QuantumPhysics <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Physics:
                    if (countOfBranches.Physics <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Philosophy:
                    if (countOfBranches.Philosophy <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Math:
                    if (countOfBranches.Math <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Chemistry:
                    if (countOfBranches.Chemistry <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Geometry:
                    if (countOfBranches.Geometry <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Biology:
                    if (countOfBranches.Biology <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Medicine:
                    if (countOfBranches.Medicine <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Evolution:
                    if (countOfBranches.Evolution <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
                case Branch.Electricity:
                    if (countOfBranches.Electricity <= 0)
                    {
                        LookSimilarOnes(); return;
                    }
                    break;
            }*/