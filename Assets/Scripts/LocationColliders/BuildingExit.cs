using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BuildingExit : MonoBehaviour
{
    [SerializeField] GameObject mainGrid = default;
    [SerializeField] GameObject houseGrid = default;
    [SerializeField] GameObject charactors = default;
    [SerializeField] CinemachineConfiner confiner = default;
    [SerializeField] Collider2D boundingShape = default;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            mainGrid.SetActive(true);
            houseGrid.SetActive(false);
            charactors.SetActive(true);
            confiner.m_BoundingShape2D = boundingShape;
        }
    }
}
