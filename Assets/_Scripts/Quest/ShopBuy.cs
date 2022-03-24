using UnityEngine;

public class ShopBuy : QuestEventListener, IShopMessage
{
    [SerializeField] string itemNotEnoughMessage = "数が足りないみたいです";
    [SerializeField] string posessionMaxMessage = "";
    [SerializeField] string closeMessage = "また売りにきてくださいね";


    Shopping shoppingUI;

    public string ItemNotEnoughMessage => itemNotEnoughMessage;

    public string PosessionMaxMessage => posessionMaxMessage;

    public string CloseMessage => closeMessage;

    void Start()
    {
        shoppingUI = QuestManager.GetQuestManager().QuestUI.Shopping;
    }

    public override void OnEventRaised()
    {
        shoppingUI.StartShoppingPlayerSell(this);
    }
}
