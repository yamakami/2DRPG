using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Fader : UIBase
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float duration;
    [SerializeField] CanvasGroup canvasGroup;

    bool fadeAvairable;

    public bool FadeAvairable { get => fadeAvairable; }

    public void FadeOut()
    {
        NotAvairable();
        canvasGroup.DOFade(0F, duration)
            .OnComplete(() => Avairable())
            .Play();
    }

    public void FadeIn()
    {
        NotAvairable();
        canvasGroup.DOFade(1F, duration)
            .OnComplete(() => Avairable())
            .Play();
    }

    void Avairable()
    {
        fadeAvairable = true;
    }

    void NotAvairable()
    {
        fadeAvairable = false;
    }

    public async void FadeOutFadeIn(MovePoint movePoint)
    {
        var playerMove = movePoint.PlayerMove;
        var locationTo = movePoint.LocationTo;
        var locationFrom = movePoint.LocationFrom;
        var facingTo = movePoint.FacingTo;
        var startPosition = movePoint.StartPosition;

        audioSource.clip = movePoint.AudioClip;
        audioSource.volume = movePoint.AudioVolume;

        playerMove.playerInfo.freeze = true;

        FadeIn();
        audioSource.Play();

        var tokenSource = new CancellationTokenSource();
        await UniTask.WaitUntil(() => FadeAvairable, cancellationToken: tokenSource.Token);

        locationTo.SetActive(true);
        playerMove.ResetPosition(facingTo, startPosition.transform.position);
        locationFrom.SetActive(false);

        await UniTask.WaitUntil(() => !audioSource.isPlaying, cancellationToken: tokenSource.Token);
        FadeOut();

        await UniTask.WaitUntil(() => FadeAvairable, cancellationToken: tokenSource.Token);

        playerMove.playerInfo.freeze = false;
    }
}
