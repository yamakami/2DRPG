using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIn : MonoBehaviour
{
    [SerializeField] GameObject locationFrom;
    [SerializeField] GameObject locationTo;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            locationFrom.gameObject.SetActive(false);
            locationTo.gameObject.SetActive(true);
        }
    }
}
