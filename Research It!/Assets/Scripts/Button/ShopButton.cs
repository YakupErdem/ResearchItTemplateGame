using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
        [HideInInspector] public bool isShopOpen;
        public GameObject workerShop;
        public float shopAnimationSpeed;
           
        public void OpenShopPage()
        {
            //Opens settings page
            if (isShopOpen || IsMenuOpen.Open) return;
            workerShop.SetActive(true);
            workerShop.transform.localScale = Vector3.zero;
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
            isShopOpen = true;
            FindObjectOfType<SetClickerActive>().Set(false);
            workerShop.transform.localScaleTransition(Vector3.one, shopAnimationSpeed);
        }
       
        public void CloseShop()
        {
            //Closes settings page
            if (!isShopOpen) return;
            workerShop.transform.localScaleTransition(Vector3.zero, shopAnimationSpeed);
            StartCoroutine(SetActiveFalse());
        }
       
        IEnumerator SetActiveFalse()
        {
            //Delaying cause of the animation of page
            yield return new WaitForSeconds(shopAnimationSpeed);
            IsMenuOpen.Open = false;
            workerShop.SetActive(false);
            FindObjectOfType<SetClickerActive>().Set(true);
            isShopOpen = false;
        }
}
