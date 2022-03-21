using UnityEngine;

public class ShopSell : QuestEventListener
{
    [SerializeField] CommandItem[] commandItems;

    ICommand[] icommands;
    Shopping shoppingUI;

    void Start()
    {
        shoppingUI = QuestManager.GetQuestManager().QuestUI.Shopping;        
        icommands = new ICommand[commandItems.Length];
        icommands = commandItems;
    }

    public override void OnEventRaised()
    {
        shoppingUI.StartShoppingPlayerBuy(icommands);
    }
}
