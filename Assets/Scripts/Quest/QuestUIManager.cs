using UnityEngine;
using System.Text;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove = default;
    [SerializeField] MessageBox messageBox;
    [SerializeField] ShopManager shopManager;

    StringBuilder stringBuilder = new StringBuilder();
    bool inTalk = false;

    public bool InTalk { get => inTalk; set => inTalk = value; }
    public StringBuilder StringBuilder { get => stringBuilder; set => stringBuilder = value; }
    public MessageBox MessageBox { get => messageBox; }

    void Start()
    {
        playerMove.QuestUIManager = this;
        playerMove.playerInfo.ShopManager = shopManager;
    }

    void Update()
    {
        ActivateMessageBox();
    }

    void ActivateMessageBox()
    {
        if (!inTalk) return;
        if (MessageBox.isActiveAndEnabled) return;

        MessageBox.PrepareConversation(playerMove.ContactWith.conversationData);
    }

    //[SerializeField] ConversationPanel conversationPanel = default;
    //[SerializeField] BattleFlash battleFlash = default;
    //[SerializeField] FaderBlack faderBlack = default;
    //[SerializeField] GameObject pristFlash = default;

    //public PlayerMove PlayerMove { get => playerMove; set => playerMove = value; }
    //public ConversationPanel ConversationPanel { get => conversationPanel; }
    //public GameObject PristFlash { get => pristFlash; }
    //public FaderBlack FaderBlack { get => faderBlack; }

    ////move to questManager
    //void ActivateBattleFlashWhite()
    //{
    //    //if (!playerMove.playerInfo.startBattle)
    //    //    return;

    //    //battleFlash.gameObject.SetActive(true);
    //}
}
