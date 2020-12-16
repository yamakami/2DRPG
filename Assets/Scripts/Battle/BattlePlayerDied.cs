
using UnityEngine;

public class BattlePlayerDied : BattleTimeline
{
    [SerializeField] Canvas playerDeadCanvas;

    public void DisplayMessage()
    {
        var bm = battleManager;

        playerDeadCanvas.enabled = true;

        PlayableStop();
        battleCanvas.MessageBox.Open();
        
        string str = "{0}はモンスター討伐に失敗した。";
        bm.BattleMessage.AppendFormat(str, bm.PlayerAction.characterName);
        messageBox.DisplayMessage(bm.BattleMessage);
    }

    public void ChageToEnd()
    {
        ChangeTimeline();
    }
}
