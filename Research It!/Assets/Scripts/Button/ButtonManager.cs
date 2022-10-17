using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Open(GameObject page, float time)
    {
        if (IsMenuOpen.Open) return;
        IsMenuOpen.Open = true;
        page.SetActive(true);
        page.transform.localScale = new Vector3(.6f,.6f,.6f);
        FindObjectOfType<SetClickerActive>().Set(false);
        page.transform.localScaleTransition(new Vector3(1.1f,1.1f,1.1f), time);
        StartCoroutine(Animation(page, time));
    }

    IEnumerator Animation(GameObject page, float time)
    {
        yield return new WaitForSeconds(time);
        page.transform.localScaleTransition(Vector3.one, time);
    }
    
    public void Close(GameObject page, float time)
    {
        if (!IsMenuOpen.Open) return;
        page.transform.localScaleTransition(Vector3.zero, time);
        StartCoroutine(SetActiveFalse(page, time));
    }

    IEnumerator SetActiveFalse(GameObject page, float time)
    {
        //Delaying cause of the animation of page.
        yield return new WaitForSeconds(time / 2.5f);
        IsMenuOpen.Open = false;
        page.SetActive(false);
        FindObjectOfType<SetClickerActive>().Set(true);
    }
}
