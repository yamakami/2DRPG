using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image image;
    [SerializeField]  Material[] materials;


    void Open()
    {
        canvas.enabled = true;

    }

    public Tween FadeToTransparent(float duration)
    {
        Open();
        image.color = new Color32(0, 0, 0, 255);

        var transparent = 0;
        return image.DOFade(transparent, duration);
    }


    public Tween FadeToBlack(float duration)
    {
        Open();
        image.color = new Color32(0, 0, 0, 0);

        var black = 1;
        return image.DOFade(black, duration);
    }

    public void FadeMaterial(int alphaTo, int materialNum)
    {
        Open();
    }
}
