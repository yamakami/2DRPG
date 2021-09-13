using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeBar : MonoBehaviour
{
    [SerializeField] RectTransform backgroundTransform;
    [SerializeField] RectTransform barTransform;
    [SerializeField] Text scoreText = default;

    [SerializeField] Text playerName;

    Vector2 backGroundSizeDelta;

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

    public void AffectValueToBar(float currentValue, float maxValue, float duration = 1f)
    {
        var newDelta = new Vector2(currentValue / maxValue * backGroundSizeDelta.x,
                                   backGroundSizeDelta.y);

        if(scoreText) scoreText.text =  currentValue + " / " + maxValue;

        barTransform.DOSizeDelta(newDelta, duration).Play();
    }
}
