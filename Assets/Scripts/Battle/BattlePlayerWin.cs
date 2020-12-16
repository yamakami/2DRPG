using UnityEngine;

public class BattlePlayerWin : BattleTimeline
{
    [SerializeField] BattleTimeline levelUpTimeline = default;

    public void PlayerWin()
    {
        var bm = battleManager;
        var playerName = bm.PlayerAction.characterName;
        var str = "{0}はモンスター討伐に成功した。";

        PlayableStop();
        messageBox.DisplayMessage(bm.BattleMessage.AppendFormat(str, playerName));
    }

    public void Reward()
    {
        var bm = battleManager;
        bm.ReflectRwardToPlayer();

        PlayableStop();
        messageBox.DisplayMessage(bm.BattleMessage);
    }

    public void IsLevelUp()
    {
        var bm = battleManager;
        if (bm.IsLevelUp())
            ChangeToLevelUp();
        else
            ChageToEnd();
    }

    void ChangeToLevelUp()
    {
        levelUpTimeline.gameObject.SetActive(true);
        levelUpTimeline.BattleManager = battleManager;
        Destroy(gameObject);
    }

    void ChageToEnd()
    {
        ChangeTimeline();
    }
}
