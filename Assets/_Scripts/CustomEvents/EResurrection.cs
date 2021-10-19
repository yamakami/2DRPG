using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public class EResurrection : MonoBehaviour
{
    [SerializeField] QuestUI questUI;
    [SerializeField] NPC npc;
    [SerializeField] ConversationData conversationData;

    BattleInfo battleInfo;

    void Start()
    {
        battleInfo = questUI.QuestManager.BattleInfo();

        if(!battleInfo.isQuestFail) return;

        StartConversation(this.GetCancellationTokenOnDestroy()).Forget();
     }

    async UniTaskVoid StartConversation(CancellationToken cancelToken)
    {
        var fader = questUI.Fader;
        await UniTask.WaitUntil(() => fader.Available(), cancellationToken: cancelToken);

        var messageBox = questUI.MessageBox;
        var player = questUI.QuestManager.Player;

        npc.Stop();
        player.TouchingToNpc(npc);

        messageBox.Activate();
        messageBox.PrepareConversation(conversationData);

        await UniTask.Delay(100, cancellationToken: cancelToken);
        battleInfo.isQuestFail = false;
    }
}
