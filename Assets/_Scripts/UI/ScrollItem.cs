using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class ScrollItem : UIBase
{
    [SerializeField] protected ScrollRect scrollRect;
    [SerializeField] Button prefTextButton;
 
    Tween tween;
    float duration = 0.5f;

    void Start()
    {
        var top = 1f;
        scrollRect.verticalNormalizedPosition = top;
    }

    abstract public void ActivateScrollItem(BattleSelector selector);

    protected Button CreateButtonUnderPanel(Transform parentTransform, string buttonText)
    {
        var button = Instantiate(prefTextButton);
        button.transform.SetParent(parentTransform);
        button.transform.localScale = Vector3.one;

        var textfield = button.GetComponentInChildren<Text>();
        textfield.text = buttonText;
        button.gameObject.SetActive(true);

        return button;
    }

    void OnDisable()
    {
        var scrollContent = scrollRect.content;
        foreach (Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void MoveUp()
    {       
        if( scrollRect.verticalNormalizedPosition < 1f ) tween = NormalizedPos(1f, duration);
    }

    public void MoveUpEnd()
    {       
        if( scrollRect.verticalNormalizedPosition < 1f ) scrollRect.verticalNormalizedPosition = 1f;
    }

    public void MoveDown()
    {
        if( 0 < scrollRect.verticalNormalizedPosition ) tween = NormalizedPos(0f, duration);
    }

    public void MoveDownEnd()
    {       
        if( 0 < scrollRect.verticalNormalizedPosition ) scrollRect.verticalNormalizedPosition = 0f;
    }

    Tween NormalizedPos(float to, float duration)
    {
            tween = scrollRect.DOVerticalNormalizedPos(to, duration);
            tween.Play();
            return tween;
    }

    public void MoveStop()
    {
        tween.Kill();
    }
}
