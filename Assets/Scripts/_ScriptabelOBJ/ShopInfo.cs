using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopInfo", menuName = "ShopInfo")]
public class ShopInfo : ScriptableObject
{
    public bool dealTypeItem;
    public bool dealTypeWeapon;
    public bool dealTypeArmor;
    public bool dealTypeInn;

    public ConversationData buyConfirm;
    public ConversationData buyComplete;
    public ConversationData buyNGShort;
    public ConversationData buyNGLimit;

    public ConversationData sellConfirm;
    public ConversationData sellComplete;
    public ConversationData sellNGItemNotEnough;
    public ConversationData sellNGNoItem;

    public Item inn;
    public ConversationData innNGShort;

    public List<Item> itemList = new List<Item>();
    public List<Item> weaponList = new List<Item>();
    public List<Item> armorList = new List<Item>();

    [HideInInspector] public int innPrice;

    public void ShopStart(PlayerInfo playerInfo)
    {
        playerInfo.ShopManager.ShopStart(this);
    }

    public void Purchase(PlayerInfo playerInfo)
    {
        playerInfo.ShopManager.ShopBuy.ClickPurchase(playerInfo);
    }

    public void SellDeal(PlayerInfo playerInfo)
    {
        playerInfo.ShopManager.ShopSell.DealOk(playerInfo);
    }

    public void ItemSelectCancel(PlayerInfo playerInfo)
    {
        playerInfo.ShopManager.ItemSelectCancel();
    }

    public void SelectBuyOrSell(PlayerInfo playerInfo)
    {
        playerInfo.ShopManager.SelectBuyOrSell.Activate();
        playerInfo.ShopManager.ItemSelect.Deactivate();
    }

    public void SetInnPrice(int price)
    {
        innPrice = price;
    }

    public void StayAtInn(PlayerInfo playerInfo)
    {
        var movePoint = playerInfo.ShopManager.PlayerMove.ContactWith.ShopInnMovePoint;
        playerInfo.ShopManager.ShopInnStart(movePoint, this);
    }
}