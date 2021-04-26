using UnityEngine;

public class SpotEnter : MonoBehaviour
{
    [SerializeField] MovePoint movePoint;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            movePoint.PlayerMove = collision.gameObject.GetComponent<PlayerMove>();
            QuestManager qm = transform.root.GetComponent<QuestManager>();
            qm.ChangeQuest(movePoint.QuestAt.name);

            movePoint.Fader.FadeOutFadeIn(movePoint);
        }
    }
}
