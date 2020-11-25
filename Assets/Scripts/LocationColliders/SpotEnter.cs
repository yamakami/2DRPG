using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotEnter : MonoBehaviour
{
    [SerializeField] FaderBlack faderBlack = default;
    [SerializeField] GameObject locationFrom = default;
    [SerializeField] GameObject locationTo = default;
    [SerializeField] Vector2 facingTo = default;
    [SerializeField] GameObject startPosition = default;

    PlayerMove playerMove;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerMove = collision.gameObject.GetComponent<PlayerMove>();
            QuestManager qm = transform.root.GetComponent<QuestManager>();
            qm.ChangeQuest(locationTo.name);

            playerMove.playerInfo.freeze = true;
            faderBlack.gameObject.SetActive(true);
            StartCoroutine(LocationChange());
        }
    }

    IEnumerator LocationChange()
    {
        while(faderBlack.gameObject.activeSelf == true)
        {
            if (faderBlack.Alpha == 1f && faderBlack.Fading == false)
            {
                locationTo.SetActive(true);
                playerMove.ResetPosition(facingTo, startPosition.transform.position);

                yield return new WaitForSeconds(1f);

                locationFrom.SetActive(false);

                faderBlack.FaderIn();

                Invoke("ReleaseFreeze", 1f);
                yield break;
            }
            yield return null;
        }
    }

    void ReleaseFreeze()
    {
        playerMove.playerInfo.freeze = false;
    }
}
