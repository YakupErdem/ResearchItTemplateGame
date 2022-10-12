using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class PlayerProfileButton : MonoBehaviour
{
           [HideInInspector] public bool isProfilePageOpen;
           public GameObject profilePage;
           public float profilePageAnimationSpeed;
           
           public void OpenProfilePage()
           {
               //Opens settings page
               if (isProfilePageOpen || IsMenuOpen.Open) return;
               IsMenuOpen.Open = true;
               isProfilePageOpen = true;
               profilePage.SetActive(true);
               profilePage.transform.localScale = Vector3.zero;
               FindObjectOfType<SetClickerActive>().Set(false);
               profilePage.transform.localScaleTransition(Vector3.one, profilePageAnimationSpeed);
           }
       
           public void CloseProfilePage()
           {
               //Closes settings page
               if (!isProfilePageOpen) return;
               profilePage.transform.localScaleTransition(Vector3.zero, profilePageAnimationSpeed);
               StartCoroutine(SetActiveFalse());
           }
       
           IEnumerator SetActiveFalse()
           {
               //Delaying cause of the animation of page
               yield return new WaitForSeconds(profilePageAnimationSpeed);
               IsMenuOpen.Open = false;
               profilePage.SetActive(false);
               FindObjectOfType<SetClickerActive>().Set(true);
               isProfilePageOpen = false;
           }
}
