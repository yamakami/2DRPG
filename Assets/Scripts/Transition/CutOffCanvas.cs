using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutOffCanvas : MonoBehaviour
{
    [SerializeField] Image cutOffImage = default;

    float MinStep = 0f;
    float transitionEnd = 1f;

    void Update()
    {
        SceneStartFadeIn();
    }

    void SceneStartFadeIn()
    {
        if (MinStep <= transitionEnd)
        {
            cutOffImage.material.SetFloat("_Duration", transitionEnd);
            transitionEnd -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
