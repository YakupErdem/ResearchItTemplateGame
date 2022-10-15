using System;
using System.Collections;
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

    [Serializable]
    public struct ResearchPages
    {
        public Sprite image;
        public Name name;
        public Description description;
        public Branch branch;
        public float moneyCost;
        public float researchCost;
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
        addedResearchPages = new List<ResearchPages>();
        SetSimilarOnes();
        GetNumberOfBranches();
        RefreshResearchPage();
    }
    //
    private void SetSimilarOnes()
    {
        similarBranches.Space = new List<Branch> { Branch.QuantumPhysics, Branch.Physics, Branch.Electricity };
        similarBranches.QuantumPhysics = new List<Branch> { Branch.Electricity, Branch.Physics, Branch.Math };
        similarBranches.Physics = new List<Branch> { Branch.QuantumPhysics, Branch.Math, Branch.Space };
        similarBranches.Philosophy = new List<Branch> { Branch.Space, Branch.Evolution };
        similarBranches.Math = new List<Branch> { Branch.Geometry, Branch.Physics };
        similarBranches.Chemistry = new List<Branch> { Branch.Medicine, Branch.Physics, Branch.Biology };
        similarBranches.Geometry = new List<Branch> { Branch.Math };
        similarBranches.Biology = new List<Branch> { Branch.Evolution, Branch.Medicine, Branch.Chemistry };
        similarBranches.Medicine = new List<Branch> { Branch.Biology, Branch.Chemistry };
        similarBranches.Evolution = new List<Branch> { Branch.Biology };
        similarBranches.Electricity = new List<Branch> { Branch.QuantumPhysics, Branch.Physics, Branch.Space };
    }
    //
    private List<ResearchPages> addedResearchPages;
    

    private void GetNumberOfBranches()
    {
        foreach (var researchPage in researchPages)
        {
            Debug.Log("Added "+ researchPage.branch + " 1");
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
    public void RefreshResearchPage()
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
            if (retriedCount + 1 >= researchPageCount)
            {
                if (!(addedResearchPages.Count >= researchPageCount))
                {
                    i--;
                }
            }
            //
            Debug.Log("Tried "+(i+1)+" Time(s)");
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
        switch (branch)
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
            }
        foreach (var researchPage in researchPages)
        {
            Debug.Log("Spawned "+ researchPage.branch);
            if (researchPage.branch == branch)
            {
                var spawnedPage = Instantiate(researchSketch, parentCanvas);
                addedResearchPages.Add(researchPage);
                switch (SaveSystem.GetString("Language"))
                {
                    case "Turkish":
                        spawnedPage.GetComponent<ResearchPageLoader>().Load(researchPage.image,
                            researchPage.name.tr,researchPage.description.tr,
                            researchPage.moneyCost,researchPage.researchCost,
                            researchPage.branch);
                        break;
                    case "English":
                        spawnedPage.GetComponent<ResearchPageLoader>().Load(researchPage.image,
                            researchPage.name.eng,researchPage.description.eng,
                            researchPage.moneyCost,researchPage.researchCost,
                            researchPage.branch);
                        break;
                }
                return;
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
        foreach (var researchPage in researchPages)
        {
            if (researchPage.branch == branch)
            {
                Debug.Log("Spawned "+ researchPage.branch);
                var spawnedPage = Instantiate(researchSketch, parentCanvas);
                addedResearchPages.Add(researchPage);
                switch (SaveSystem.GetString("Language"))
                {
                    case "Turkish":
                        spawnedPage.GetComponent<ResearchPageLoader>().Load(researchPage.image,
                            researchPage.name.tr,researchPage.description.tr,
                            researchPage.moneyCost,researchPage.researchCost,
                            researchPage.branch);
                        break;
                    case "English":
                        spawnedPage.GetComponent<ResearchPageLoader>().Load(researchPage.image,
                            researchPage.name.eng,researchPage.description.eng,
                            researchPage.moneyCost,researchPage.researchCost,
                            researchPage.branch);
                        break;
                }
                break;
            }
        }
    }

    private int similarCount;
    private void LookSimilarOnes()
    {
        Debug.Log("Looking to similar ones "+ similarCount);
        //
        switch (testBranch)
            {
                case Branch.Space:
                    Spawn(similarBranches.Space[Random.Range(0, similarBranches.Space.Count)]);
                    break;
                case Branch.QuantumPhysics:
                    Spawn(similarBranches.QuantumPhysics[Random.Range(0, similarBranches.QuantumPhysics.Count)]);
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
                    Spawn(similarBranches.Philosophy[Random.Range(0, similarBranches.Philosophy.Count)]);
                    break;
                case Branch.Math:
                    Spawn(similarBranches.Math[Random.Range(0, similarBranches.Math.Count)]);
                    break;
                case Branch.Chemistry:
                    Spawn(similarBranches.Chemistry[Random.Range(0, similarBranches.Chemistry.Count)]);
                    break;
                case Branch.Geometry:
                    Spawn(similarBranches.Geometry[Random.Range(0, similarBranches.Geometry.Count)]);
                    break;
                case Branch.Biology:
                    Spawn(similarBranches.Biology[Random.Range(0, similarBranches.Biology.Count)]);
                    break;
                case Branch.Medicine:
                    Spawn(similarBranches.Medicine[Random.Range(0, similarBranches.Medicine.Count)]);
                    break;
                case Branch.Evolution:
                    Spawn(similarBranches.Evolution[Random.Range(0, similarBranches.Evolution.Count)]);
                    break;
                case Branch.Electricity:
                    Spawn(similarBranches.Electricity[Random.Range(0, similarBranches.Electricity.Count)]);
                    break;
            }
        similarCount++;
    }
}
