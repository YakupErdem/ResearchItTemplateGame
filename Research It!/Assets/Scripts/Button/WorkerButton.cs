using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class WorkerButton : MonoBehaviour
{
    [HideInInspector] public bool isWorkerPageOpen;
    public GameObject workerPage;
    public float workerPageAnimationSpeed;
       
    public void OpenWorkerPage(GameObject buttonObject)
    {
        //Opens settings page
        FindObjectOfType<ButtonManager>().Open(workerPage, workerPageAnimationSpeed);
        /*
        if (isWorkerPageOpen || IsMenuOpen.Open) return;
        IsMenuOpen.Open = true;
        isWorkerPageOpen = true;
        buttonObject.transform.localScaleTransition(new Vector3(.7f, .7f, .7f), .2f);
        StartCoroutine(Animation(buttonObject));*/
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Animation(GameObject g)
    {
        yield return new WaitForSeconds(.2f);
        g.transform.localScaleTransition(Vector3.one, .2f);
        FindObjectOfType<RefreshWorkerPage>().RefreshWorkers();
        
        /*workerPage.SetActive(true);
        workerPage.transform.localScale = Vector3.zero;
        FindObjectOfType<SetClickerActive>().Set(false);
        workerPage.transform.localScaleTransition(Vector3.one, workerPageAnimationSpeed);*/
    }
   
    public void CloseWorkerPage()
    {
        //Closes settings page
        FindObjectOfType<ButtonManager>().Close(workerPage, workerPageAnimationSpeed);
        /*if (!isWorkerPageOpen) return;
        workerPage.transform.localScaleTransition(Vector3.zero, workerPageAnimationSpeed);
        StartCoroutine(SetActiveFalse());*/
    }
   
    IEnumerator SetActiveFalse()
    {
        //Delaying cause of the animation of page
        yield return new WaitForSeconds(workerPageAnimationSpeed);
        IsMenuOpen.Open = false;
        workerPage.SetActive(false);
        FindObjectOfType<SetClickerActive>().Set(true);
        isWorkerPageOpen = false;
    }
}
