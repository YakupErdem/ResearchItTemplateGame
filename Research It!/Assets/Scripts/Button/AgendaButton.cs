using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class AgendaButton : MonoBehaviour
{
       [HideInInspector] public bool isAgendaOpen;
       public GameObject agendaPage;
       public float agendaPageAnimationSpeed;
       
       public void OpenAgenda()
       {
           //Opens settings page
           FindObjectOfType<ButtonManager>().Open(agendaPage, agendaPageAnimationSpeed);
           /*if (isAgendaOpen || IsMenuOpen.Open) return;
           IsMenuOpen.Open = true;
           isAgendaOpen = true;
           agendaPage.SetActive(true);
           agendaPage.transform.localScale = Vector3.zero;
           FindObjectOfType<SetClickerActive>().Set(false);
           agendaPage.transform.localScaleTransition(Vector3.one, agendaPageAnimationSpeed);*/
       }
   
       public void CloseAgenda()
       {
           //Closes settings page
           FindObjectOfType<ButtonManager>().Close(agendaPage, agendaPageAnimationSpeed);
           /*
           if (!isAgendaOpen) return;
           agendaPage.transform.localScaleTransition(Vector3.zero, agendaPageAnimationSpeed);
           StartCoroutine(SetActiveFalse());*/
       }
   
       IEnumerator SetActiveFalse()
       {
           //Delaying cause of the animation of page
           yield return new WaitForSeconds(agendaPageAnimationSpeed);
           IsMenuOpen.Open = false;
           agendaPage.SetActive(false);
           FindObjectOfType<SetClickerActive>().Set(true);
           isAgendaOpen = false;
       }
}
