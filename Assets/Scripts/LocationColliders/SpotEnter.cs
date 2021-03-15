using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SpotEnter : MonoBehaviour
{
    [SerializeField] Fader fader = default;
    [SerializeField] GameObject locationFrom = default;
    [SerializeField] GameObject locationTo = default;
    [SerializeField] Vector2 facingTo = default;
    [SerializeField] GameObject startPosition = default;

    PlayerMove playerMove;

    async void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerMove = collision.gameObject.GetComponent<PlayerMove>();
            QuestManager qm = transform.root.GetComponent<QuestManager>();
            qm.ChangeQuest(locationTo.name);

            playerMove.playerInfo.freeze = true;

            fader.FadeIn();
            var tokenSource = new CancellationTokenSource();
            await UniTask.WaitUntil(() => fader.FadeAvairable, cancellationToken: tokenSource.Token);

            locationTo.SetActive(true);
            playerMove.ResetPosition(facingTo, startPosition.transform.position);
            locationFrom.SetActive(false);

            await UniTask.Delay(500, cancellationToken: tokenSource.Token);

            fader.FadeOut();
            await UniTask.WaitUntil(() => fader.FadeAvairable, cancellationToken: tokenSource.Token);

            await UniTask.Delay(500, cancellationToken: tokenSource.Token);

            playerMove.playerInfo.freeze = false;
        }
    }
}
