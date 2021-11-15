using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class QuestMenuStatus : MonoBehaviour
{
    [SerializeField] QuestUI questUI;
    [SerializeField] Text lv;
    [SerializeField] Text hP;
    [SerializeField] Text mP;
    [SerializeField] Text maxHP;
    [SerializeField] Text maxMP;
    [SerializeField] Text attack;
    [SerializeField] Text defence;
    [SerializeField] Text exp;
    [SerializeField] Text gold;
    [SerializeField] Text nextLevelUp;

    StringBuilder stringBuilder;

    void Awake()
    {
        stringBuilder = new StringBuilder();
    }

    void OnEnable()
    {
        var questManager = questUI.QuestManager;
        var playerInfo = questManager.PlayerInfo();

        SetStatusText(questManager, playerInfo);
    }

    void SetStatusText(QuestManager questManager, PlayerInfo playerInfo)
    {
        maxHP.text = FormatString("最大HP {0}", playerInfo.status.maxHP);
        hP.text = FormatString("HP {0}", playerInfo.status.hp);
        maxMP.text = FormatString("最大MP {0}", playerInfo.status.maxMP);
        mP.text = FormatString("MP {0}", playerInfo.status.mp);
        attack.text = FormatString("攻撃力 {0}", playerInfo.status.attack);
        defence.text = FormatString("守備力 {0}", playerInfo.status.defence);
        exp.text = FormatString("経験値 {0}", playerInfo.status.exp);
        gold.text = FormatString("ゴールド {0}", playerInfo.status.gold);

        var playerLevel = playerInfo.status.lv;
        var nextLevelExp = questManager.GameInfo().levelUpTable.levels[playerLevel].minimumExp;
        nextLevelUp.text = FormatString("レベルアップまで残り{0}EXP", nextLevelExp - playerInfo.status.exp);
    }

    string FormatString(string str, params object[] args)
    {
        stringBuilder.Clear();
        return stringBuilder.AppendFormat(str, args).ToString();
    }
}
