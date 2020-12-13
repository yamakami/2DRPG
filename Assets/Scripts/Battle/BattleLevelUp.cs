using UnityEngine;
using UnityEngine.Playables;

public class BattleLevelUp : BattleTimeline
{
    public void LevelUp()
    {
        var bm = battleManager;

        string playerName = bm.PlayerAction.characterName;
        int playerExp = bm.PlayerAction.exp;
        LevelUpTable.Level currentLevel = bm.LevelUpTable.levels[bm.PlayerAction.lv];

        string str;
        if (currentLevel.goalExp <= playerExp)
        {
            bm.PlayerAction.lv++;
            currentLevel = bm.LevelUpTable.levels[bm.PlayerAction.lv];

            str = "{0}はレベルが{1}になった\n";
            bm.BattleMessage.AppendFormat(str, playerName, bm.PlayerAction.lv);

            if (0 < currentLevel.hp)
            {
                bm.PlayerAction.maxHP += currentLevel.hp;
                str = "HPが{0}上がった\n";
                bm.BattleMessage.AppendFormat(str, currentLevel.hp);
            }

            if (0 < currentLevel.mp)
            {
                bm.PlayerAction.maxMP += currentLevel.mp;
                str = "MPが{0}上がった\n";
                bm.BattleMessage.AppendFormat(str, currentLevel.mp);
            }

            if (0 < currentLevel.attack)
            {
                bm.PlayerAction.attack += currentLevel.attack;
                str = "攻撃力が{0}上がった\n";
                bm.BattleMessage.AppendFormat(str, currentLevel.attack);
            }

            if (0 < currentLevel.attack)
            {
                bm.PlayerAction.defence += currentLevel.defence;
                str = "守備力が{0}上がった\n";
                bm.BattleMessage.AppendFormat(str, currentLevel.defence);
            }
        }
    }

    public void ShowMessage()
    {
        battleManager.PlayableStop();
        messageBox.DisplayMessage(battleManager.BattleMessage);
    }

    public void LevelUpRepeat()
    {
        if (!battleManager.IsLevelUp())
            ChangeTimeline();
    }
}
