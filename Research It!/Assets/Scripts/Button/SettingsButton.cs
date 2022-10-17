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
        //Opens settings page.
        FindObjectOfType<ButtonManager>().Open(settingsPage, settingsPageAnimationSpeed);

        /*
        if (isSettingsOpen || IsMenuOpen.Open) return;
        IsMenuOpen.Open = true;
        isSettingsOpen = true;
        settingsPage.SetActive(true);
        settingsPage.transform.localScale = new Vector3(.6f,.6f,.6f);
        FindObjectOfType<SetClickerActive>().Set(false);
        settingsPage.transform.localScaleTransition(new Vector3(1.1f,1.1f,1.1f), settingsPageAnimationSpeed);
        StartCoroutine(Animation());*/
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(settingsPageAnimationSpeed);
        settingsPage.transform.localScaleTransition(Vector3.one, settingsPageAnimationSpeed);
    }

    public void CloseSettings()
    {
        //Closes settings page.
        FindObjectOfType<ButtonManager>().Close(settingsPage, settingsPageAnimationSpeed);
        /*
        if (!isSettingsOpen) return;
        settingsPage.transform.localScaleTransition(Vector3.zero, settingsPageAnimationSpeed);
        StartCoroutine(SetActiveFalse());*/
    }

    IEnumerator SetActiveFalse()
    {
        //Delaying cause of the animation of page.
        yield return new WaitForSeconds(settingsPageAnimationSpeed / 2.5f);
        IsMenuOpen.Open = false;
        settingsPage.SetActive(false);
        FindObjectOfType<SetClickerActive>().Set(true);
        isSettingsOpen = false;
    }
}
