using UnityEngine;

public class ShopBuy : QuestEventListener, IShopMessage
{
    [SerializeField] string itemNotEnoughMessage = "足りないみたいです";
    [SerializeField] string posessionMaxMessage = "{0}はこれ以上持てません";
    [SerializeField] string closeMessage = "またきてくれよな";

    public string ItemNotEnoughMessage => itemNotEnoughMessage;

    public string PosessionMaxMessage => posessionMaxMessage;

    public string CloseMessage => closeMessage;

    public override void OnEventRaised()
    {

Debug.Log("------------shop buy");
    }
}
