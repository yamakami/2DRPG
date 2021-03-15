using UnityEngine;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] CanvasGroup canvasGroup;

    bool fadeAvairable;

    public bool FadeAvairable { get => fadeAvairable; }

    public void FadeOut()
    {
        fadeAvairable = false;
        canvasGroup.DOFade(0F, duration)
            .OnComplete(() => fadeAvairable = true)
            .Play();
    }

    public void FadeIn()
    {
        fadeAvairable = false;
        canvasGroup.DOFade(1F, duration)
            .OnComplete(() => fadeAvairable = true)
            .Play();
    }
}
