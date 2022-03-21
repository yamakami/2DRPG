using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTotalAmount : MonoBehaviour
{
    [SerializeField] Text totalPriceText;
    [SerializeField] AudioClip shopSound;
    QuestManager questManager;
    PlayerInfo playerInfo;
    int totalPrice;
    CommandItem selectedItme;

    public void Visible(QuestManager questManager, PlayerInfo playerInfo, CommandItem selectedItme, int selectedAmount, int totalPrice)
    {
        gameObject.SetActive(true);
        this.questManager = questManager;
        this.playerInfo = playerInfo;
        this.selectedItme = selectedItme;
        this.totalPrice = totalPrice;

        totalPriceText.text = totalPrice.ToString();
    }

    public void Buy()
    {
        selectedItme.player_possession_count++;

        var playerItems = playerInfo.items;
        if(!playerItems.Find( i => i == selectedItme)) playerItems.Add(selectedItme);

        playerInfo.status.gold -= totalPrice;
        questManager.QuestUI.SeAudioSource.PlayOneShot(shopSound);
    }
}
