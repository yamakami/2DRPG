using UnityEngine;
using UnityEngine.UI;

public class QuestStatus : MonoBehaviour
{
    [SerializeField] Canvas cavas;
    [SerializeField] Text lv;
    [SerializeField] Text nextLv;
    [SerializeField] Text maxHp;
    [SerializeField] Text hp;
    [SerializeField] Text maxMp;
    [SerializeField] Text mp;
    [SerializeField] Text attack;
    [SerializeField] Text defence;

    public void Open()
    {
        cavas.enabled = true;
        Status();
    }

    void Status()
    {
        var status =  QuestManager.GetQuestManager().Player.PlayerInfo.status;

        lv.text = status.lv.ToString();
        maxHp.text = status.maxHP.ToString();
        hp.text = status.hp.ToString();
        maxMp.text = status.maxMP.ToString();
        attack.text = status.attack.ToString();
        defence.text = status.defence.ToString();

        nextLv.text = "dummy";
    }
}
