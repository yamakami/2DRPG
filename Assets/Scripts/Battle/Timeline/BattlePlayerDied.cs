
using UnityEngine;

public class BattlePlayerDied : BattleTimeline
{
    [SerializeField] Canvas playerDeadCanvas = default;

    public void DisplayMessage()
    {
        var bm = battleManager;

        playerDeadCanvas.enabled = true;
        bm.PlayerAction.playerInfo.dead = true;

        PlayableStop();
        battleCanvas.MessageText.Activate();

        string str = "{0}はモンスター討伐に失敗した。";
        bm.BattleMessage.AppendFormat(str, bm.PlayerAction.characterName);
        battleCanvas.MessageText.DisplayBattleMessage(bm.BattleMessage);

        RestoreStatus(bm);
    }

    void RestoreStatus(BattleManager bm)
    {
        bm.PlayerAction.hp = bm.PlayerAction.maxHP;
        bm.PlayerAction.mp = bm.PlayerAction.maxMP;
        bm.PlayerAction.gold = Mathf.FloorToInt(bm.PlayerAction.gold / 3);
    }

    public void ChageToEnd()
    {
        ChangeTimeline();
    }
}
