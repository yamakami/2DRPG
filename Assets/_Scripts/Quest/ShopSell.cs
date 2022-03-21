using UnityEngine;

public class ShopSell : QuestEventListener, IShopMessage
{
    [SerializeField] CommandItem[] commandItems;
    [SerializeField] string itemNotEnoughMessage = "お金が足りないみたいです";
    [SerializeField] string posessionMaxMessage = "{0}はこれ以上持てません";
    [SerializeField] string closeMessage = "またきてくれよな";

    ICommand[] icommands;
    Shopping shoppingUI;

    public string ItemNotEnoughMessage => itemNotEnoughMessage;

    public string PosessionMaxMessage => posessionMaxMessage;

    public string CloseMessage => closeMessage;

    void Start()
    {
        shoppingUI = QuestManager.GetQuestManager().QuestUI.Shopping;
        icommands = new ICommand[commandItems.Length];
        icommands = commandItems;
    }

    public override void OnEventRaised()
    {
        shoppingUI.StartShoppingPlayerBuy(icommands, this);
    }
}
