public class ShopBuy : UIBase
{
    public void ClickPurchase(PlayerInfo playerInfo)
    {
        var shopManager     = playerInfo.ShopManager;
        var questUIManager  = shopManager.QuestUIManager;
        var messgeBox       = questUIManager.MessageBox;
        var shopInfo        = shopManager.ShopInfo;
        var selectedItem    = shopManager.SelectedItem;
        var itemTotalPrice  = shopManager.ItemTotalPrice;
        var selectedItemNum = shopManager.SelectedItemNum;

        shopManager.PlayerAndTargetCharFreeze();
        messgeBox.SortOrderFront();

        if (ItemNumOver(selectedItem, selectedItemNum))
        {
            var stBuilder = questUIManager.StringBuilder;
            stBuilder.Clear();
            shopInfo.buyNGLimit.conversationLines[0].text = stBuilder.AppendFormat(
                shopInfo.buyNGLimit.dynamicText, selectedItem.nameKana, selectedItemNum).ToString();

            messgeBox.PrepareConversation(shopInfo.buyNGLimit);
        }
        else if (playerInfo.status.gold < itemTotalPrice)
        {
            messgeBox.PrepareConversation(shopInfo.buyNGShort);
        }
        else
        {
            SubtructGold(shopManager);
            AddItemToPlayer(playerInfo, selectedItem, selectedItemNum);
            messgeBox.PrepareConversation(shopInfo.buyComplete);
        }
    }

    bool ItemNumOver(Item selectedItem, int selectedItemNum)
    {
        if (selectedItem.player_possession_limit == 0) return false;
        if (selectedItem.player_possession_limit < (selectedItem.player_possession_count + selectedItemNum))
            return true;

        return false;
    }

    void SubtructGold(ShopManager shopManager)
    {
        var itemSelect = shopManager.ItemSelect;
        var playerMove = shopManager.PlayerMove;
        var playerInfo = playerMove.playerInfo;

        playerMove.playerInfo.status.gold -= shopManager.ItemTotalPrice;

        var stBuilder = playerMove.QuestUIManager.StringBuilder;
        stBuilder.Clear();

        itemSelect.Golds.text = stBuilder.AppendFormat("{0} G", playerInfo.status.gold).ToString();
    }

    // battleでも使うのでplayerMoveにいどう
    void AddItemToPlayer(PlayerInfo playerInfo, Item selectedItem, int selectedItemNum)
    {
        Item item = playerInfo.items.Find(i => i.itemName == selectedItem.itemName);
        if (item != null)
        {
            var limit = item.player_possession_limit;
            var ct = item.player_possession_count;
            if (limit == 0 || ct < limit)
                item.player_possession_count += selectedItemNum;
        }
        else
        {
            selectedItem.player_possession_count += selectedItemNum;
            playerInfo.items.Add(selectedItem);
        }
    }
}
