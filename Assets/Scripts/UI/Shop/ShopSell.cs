public class ShopSell : UIBase
{
    public void DealOk(PlayerInfo playerInfo)
    {
        var shopManager = playerInfo.ShopManager;
        var shopInfo = shopManager.ShopInfo;
        var messgeBox = shopManager.QuestUIManager.MessageBox;
        var selectedItem = shopManager.SelectedItem;
        var selectedItemNum = shopManager.SelectedItemNum;

        shopManager.PlayerAndTargetCharFreeze();

        messgeBox.SortOrderFront();

        if (selectedItem.player_possession_count < selectedItemNum)
        {
            messgeBox.PrepareConversation(shopInfo.sellNGItemNotEnough);
            return;
        }
        else
        {
            RemoveFromPlayer(shopManager, selectedItem, selectedItemNum);

            if (playerInfo.items.Count == 0)
                messgeBox.PrepareConversation(shopInfo.sellNGNoItem);
            else
                messgeBox.PrepareConversation(shopInfo.sellComplete);
        }
    }

    void RemoveFromPlayer(ShopManager shopManager, Item selectedItem, int selectedItemNum)
    {
        var playerInfo = shopManager.PlayerMove.playerInfo;

        playerInfo.status.gold += shopManager.ItemTotalPrice;

        selectedItem.player_possession_count -= selectedItemNum;

        if (selectedItem.player_possession_count < 1)
            playerInfo.items.Remove(selectedItem);

        var itemSelect = shopManager.ItemSelect;
        itemSelect.Deactivate();

        if (0 < playerInfo.items.Count)
            itemSelect.Activate();
    }
}
