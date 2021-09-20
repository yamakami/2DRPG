using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeBar : MonoBehaviour
{
    [SerializeField] RectTransform backgroundTransform;
    [SerializeField] RectTransform barTransform;
    [SerializeField] Image barImage = default;
    [SerializeField] Text scoreText = default;

    [SerializeField] Text playerName;

    Vector2 backGroundSizeDelta;
    Color32 barColor;

    public Text PlayerName { get => playerName; }

    void Awake()
    {
        backGroundSizeDelta = backgroundTransform.sizeDelta;
    }

    public void SetMonsterLifeBarPosition(SpriteRenderer sp)
    {
        var canvasCurrentPosition = transform.position;
        transform.position = new Vector2(canvasCurrentPosition.x,
                                         canvasCurrentPosition.y  + sp.bounds.size.y / 2);
    }

    public void AffectValueToBar(float currentValue, float maxValue, float duration = 0.7f)
    {
        var lifeValueRatio = currentValue / maxValue;
        var newDelta = new Vector2(backGroundSizeDelta.x * lifeValueRatio, backGroundSizeDelta.y);

        if(scoreText) scoreText.text =  Mathf.Clamp(currentValue, 0, maxValue)  + " / " + maxValue;

        barTransform.DOSizeDelta(newDelta, duration)
                    .OnComplete(() => SetBarColor(lifeValueRatio)).Play();
    }

    public void SetBarColor (float lifeRatio)
    {
        if(!barImage) return;

        var zoneGreen = 0.5f;
        var colorSliderMin = 0f;
        var colorSlideMax = 255f;
        var sliderLength = (colorSlideMax - colorSliderMin) * 2;
        var sliderPosition = 0f;

        var color = new Color32();
        color.a = 255;

        if(zoneGreen <= lifeRatio)
        {
            var upperRatio = 1f - lifeRatio;
            sliderPosition = colorSliderMin + Mathf.Floor(sliderLength * upperRatio);

            color.r = (byte) sliderPosition;
            color.g = 255;
            color.b = 5;
        }
        else
        {
            sliderPosition = colorSliderMin + Mathf.Floor(sliderLength * lifeRatio);
            color.r = 255;
            color.g = (byte) sliderPosition;
            color.b = 0;
        }

        barImage.color = color;
    }
}
