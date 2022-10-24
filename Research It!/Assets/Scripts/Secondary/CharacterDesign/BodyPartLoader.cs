using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartLoader : MonoBehaviour
{
    public Image image;
    public float animationSpeed;
    
    public void Load(Sprite sprite)
    {
        image.sprite = sprite;
        transform.localScale = new Vector3(.6f, .6f, .6f);
        transform.localScaleTransition(new Vector3(1.1f, 1.1f, 1.1f), animationSpeed);
        StartCoroutine(Animation());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(.1f);
        transform.localScaleTransition(Vector3.one, animationSpeed);
    }
}
