using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTouching : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2 = default;
    [HideInInspector] public bool otherNpcTouching;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb2.constraints = RigidbodyConstraints2D.FreezeAll;
            collision.GetComponent<PlayerMove>().TouchingToNpc(GetComponent<CharacterMove>());
        }

        if (collision.CompareTag("Npc"))
            otherNpcTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
            collision.GetComponent<PlayerMove>().TouchingToNpc(null);
        }

        if (collision.CompareTag("Npc"))
            otherNpcTouching = false;
    }
}
