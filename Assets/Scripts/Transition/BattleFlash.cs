using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleFlash : MonoBehaviour
{    
    [SerializeField] [Range(0f, 1f)] float fadeTo;
    [SerializeField] Color color;
    [SerializeField] Image image;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float duration;
    [SerializeField] Ease easeType;
    [SerializeField] LoopType loopType;
    [SerializeField] int repeat;

    bool blinkAvairable;

    public bool BlinkAvairable { get => blinkAvairable; }

    void OnEnable()
    {
        Blink();
    }

    public void Blink()
    {
        NotAvairable();

        image.color = color;
        canvasGroup.DOFade(fadeTo, duration)
            .SetEase(easeType)
            .SetLoops(repeat, loopType)
            .OnComplete(() => Avairable())
            .Play();
    }

    void Avairable()
    {
        blinkAvairable = true;
    }

    void NotAvairable()
    {
        blinkAvairable = false;
    }
}
