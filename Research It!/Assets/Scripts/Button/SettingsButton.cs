using System.Collections;
using Lean.Transition;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [HideInInspector] public bool isSettingsOpen;
    public GameObject settingsPage;
    public float settingsPageAnimationSpeed;
    
    public void OpenSettings()
    {
        //Opens settings page
        if (isSettingsOpen || IsMenuOpen.Open) return;
        IsMenuOpen.Open = true;
        isSettingsOpen = true;
        settingsPage.SetActive(true);
        settingsPage.transform.localScale = Vector3.zero;
        FindObjectOfType<SetClickerActive>().Set(false);
        settingsPage.transform.localScaleTransition(Vector3.one, settingsPageAnimationSpeed);
    }

    public void CloseSettings()
    {
        //Closes settings page
        if (!isSettingsOpen) return;
        settingsPage.transform.localScaleTransition(Vector3.zero, settingsPageAnimationSpeed);
        StartCoroutine(SetActiveFalse());
    }

    IEnumerator SetActiveFalse()
    {
        //Delaying cause of the animation of page
        yield return new WaitForSeconds(settingsPageAnimationSpeed);
        IsMenuOpen.Open = false;
        settingsPage.SetActive(false);
        FindObjectOfType<SetClickerActive>().Set(true);
        isSettingsOpen = false;
    }
}
