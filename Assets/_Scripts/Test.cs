using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Test : MonoBehaviour
{
    [SerializeField]  Image image;

    Color color;
    void Start()
    {
        color = image.color;


    }
}
