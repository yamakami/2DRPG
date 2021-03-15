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
    public ConversationData buyNG;

    public Item inn;
    public List<Item> itemList = new List<Item>();
    public List<Item> weaponList = new List<Item>();
    public List<Item> armorList = new List<Item>();

    public void ShopStart(PlayerInfo playerInfo)
    {
        playerInfo.ShopManager.ShopStart(this);
    }

    public void PurchaseItem(PlayerInfo playerInfo)
    {
        Debug.Log("----------purchace click");
        playerInfo.ShopManager.BuyOK();
    }

    public void PurchaseCancel(PlayerInfo playerInfo)
    {
        Debug.Log("----------purchace  cahcel click");
        playerInfo.ShopManager.OnBuyCancel();
    }

}

