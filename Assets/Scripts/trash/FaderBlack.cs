using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderBlack : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup = default;
    [SerializeField] float speed = 1f;
    [SerializeField] float fadeInSpeed = 1f;

    bool fading;
    public bool Fading{ get { return fading; } }
    public float Alpha { get { return canvasGroup.alpha; } set { canvasGroup.alpha = value; } }

    bool skipFadeOut;
    public bool SkipFadeOut { get => skipFadeOut; set => skipFadeOut = value; }

    void OnEnable()
    {
        if (!SkipFadeOut)
            StartCoroutine(FadeOut());

        skipFadeOut = false;
    }

    IEnumerator FadeOut()
    {
        fading = true;
        while (canvasGroup.alpha <= 1)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            if (1 == canvasGroup.alpha)
            {
                fading = false;
                yield break;
            }
            yield return null;
        }
    }

    public void FaderIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (0 <= canvasGroup.alpha)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeInSpeed;
            if (canvasGroup.alpha == 0)
            {
                fading = false;
                gameObject.SetActive(false);
                yield break;
            }

            yield return null;
        }
    }
}
