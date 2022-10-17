using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class ResearchPageButton : MonoBehaviour
{
       [HideInInspector] public bool isResearchPageOpen;
       public GameObject researchPage;
       public float researchPageAnimationSpeed;
       
       public void OpenResearch()
       {
           //Opens settings page
           FindObjectOfType<ButtonManager>().Open(researchPage, researchPageAnimationSpeed);
           /*
           if (isResearchPageOpen || IsMenuOpen.Open) return;
           IsMenuOpen.Open = true;
           isResearchPageOpen = true;
           researchPage.SetActive(true);
           researchPage.transform.localScale = Vector3.zero;
           FindObjectOfType<SetClickerActive>().Set(false);
           researchPage.transform.localScaleTransition(Vector3.one, researchPageAnimationSpeed);*/
       }
   
       public void CloseResearch()
       {
           //Closes settings page
           FindObjectOfType<ButtonManager>().Close(researchPage, researchPageAnimationSpeed);
           /*
           if (!isResearchPageOpen) return;
           researchPage.transform.localScaleTransition(Vector3.zero, researchPageAnimationSpeed);
           StartCoroutine(SetActiveFalse());*/
       }
   
       IEnumerator SetActiveFalse()
       {
           //Delaying cause of the animation of page
           yield return new WaitForSeconds(researchPageAnimationSpeed);
           IsMenuOpen.Open = false;
           researchPage.SetActive(false);
           FindObjectOfType<SetClickerActive>().Set(true);
           isResearchPageOpen = false;
       }
}
