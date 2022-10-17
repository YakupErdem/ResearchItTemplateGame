using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [HideInInspector] public bool isShopOpen;
    public GameObject shopPage;
    public float shopAnimationSpeed;

    public void OpenShopPage()
    {
        //Opens settings page
        if (isShopOpen || IsMenuOpen.Open) return;
        shopPage.SetActive(true);
        shopPage.transform.localScale = new Vector3(.6f, .6f, .6f);
        //
        IsMenuOpen.Open = true;
        isShopOpen = true;
        FindObjectOfType<SetClickerActive>().Set(false);
        shopPage.transform.localScaleTransition(new Vector3(1.1f, 1.1f, 1.1f), shopAnimationSpeed);
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(shopAnimationSpeed);
        shopPage.transform.localScaleTransition(Vector3.one, shopAnimationSpeed);
    }

    public void CloseShop()
    {
        //Closes settings page
        if (!isShopOpen) return;
        shopPage.transform.localScaleTransition(Vector3.zero, shopAnimationSpeed);
        StartCoroutine(SetActiveFalse());
    }

    IEnumerator SetActiveFalse()
    {
        //Delaying cause of the animation of page
        yield return new WaitForSeconds(shopAnimationSpeed);
        IsMenuOpen.Open = false;
        shopPage.SetActive(false);
        FindObjectOfType<SetClickerActive>().Set(true);
        isShopOpen = false;
    }
}