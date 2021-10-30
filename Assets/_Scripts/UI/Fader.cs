using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fader : UIBase
{
     [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image image;
    [SerializeField] Material cutOffShader;
    [SerializeField] float duration = 1f;
    [SerializeField] AudioSource audioSource;

    bool fading = default;

    public void Fade(float alpha, float delay = 0)
    {
        FadeStart();
        canvasGroup.DOFade(alpha, duration)
                   .SetDelay(delay)
                   .OnStart(() => audioSource.Play())
                   .OnComplete(() => FadeEnd())
                   .Play();
    }

    public void Flash(Color32 color, float alpha, float delay = 0)
    {
        FadeStart();
        image.color = color;

        var zigzagAmplitude = 7;
        canvasGroup.DOFade(alpha, duration)
                   .SetEase(Ease.Flash, zigzagAmplitude)
                   .OnComplete(() => FadeEnd())
                   .Play();
    }

    public void CutOff()
    {
        FadeStart();
        image.material = cutOffShader;

        var notTransparent = 1f;
        canvasGroup.alpha = notTransparent;
        cutOffShader.SetFloat("_Duration", notTransparent);

        var transparent = 0f;
        var duration = 1.5f;
        cutOffShader.DOFloat(transparent, "_Duration", duration)
                    .OnComplete(() => CutOffEnd())
                    .Play();
    }

    public Fader SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        return this;
    }

    void FadeStart()
    {
        fading = true;
        canvas.sortingOrder = 99;
    }

    void FadeEnd()
    {
        fading = false;
        StopAudio();
    }

    void CutOffEnd()
    {
        FadeEnd();

        image.material = null;
        var transparent = 0f;
        cutOffShader.SetFloat("_Duration", transparent);
        canvasGroup.alpha = transparent;
    }

   void StopAudio()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public bool Available()
    {
        if (!fading && !audioSource.isPlaying)
        {
            canvas.sortingOrder = 5;
            return true;
        }


        return false;
    }

    public void SetAlpha(int alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
