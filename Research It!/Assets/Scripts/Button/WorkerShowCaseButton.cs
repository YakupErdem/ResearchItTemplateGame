using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class WorkerShowCaseButton : MonoBehaviour
{
        [HideInInspector] public bool isWorkerPageOpen;
        public GameObject workerPage;
        public float workerPageAnimationSpeed;
           
        public void OpenWorkerPage()
        {
            //Opens settings page
            if (isWorkerPageOpen || IsMenuOpen.Open) return;
            workerPage.SetActive(true);
            workerPage.transform.localScale = Vector3.zero;
            //
            if (WorkerReload.RefreshPage)
            {
                foreach (var info in WorkerReload.infos)
                {
                    Destroy(info);
                }
                FindObjectOfType<WorkerReload>().Refresh();
            }
            IsMenuOpen.Open = true;
            isWorkerPageOpen = true;
            FindObjectOfType<SetClickerActive>().Set(false);
            workerPage.transform.localScaleTransition(Vector3.one, workerPageAnimationSpeed);
        }
       
        public void CloseWorkerPage()
        {
            //Closes settings page
            if (!isWorkerPageOpen) return;
            workerPage.transform.localScaleTransition(Vector3.zero, workerPageAnimationSpeed);
            StartCoroutine(SetActiveFalse());
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
