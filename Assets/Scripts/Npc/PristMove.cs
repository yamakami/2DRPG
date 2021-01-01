using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PristMove : CharacterMove
{
    [SerializeField] CanvasManager questCanvas = default;
    [SerializeField] PlayableDirector playableDirector = default;
    [SerializeField] ConversationData regularConversation = default;
    [SerializeField] ConversationData reviveConversation = default;

    protected override void Start()
    {
        base.Start();

        if (!questCanvas.PlayerMove.playerInfo.dead)
        {
            conversationData = regularConversation;
        }
        else
        {
            conversationData = reviveConversation;
            StartCoroutine(PlayerRevive());
        }
    }

    IEnumerator PlayerRevive()
    {
        while (questCanvas.FaderBlack.isActiveAndEnabled)
            yield return null;

        questCanvas.PlayerMove.playerInfo.dead = false;
        freeze = true;
        questCanvas.ConversationPanel.MessageBox.NextButton.gameObject.SetActive(false);
        questCanvas.PlayerMove.playerInfo.startConversation = true;
        questCanvas.PlayerMove.TouchingToNpc(this);
        playableDirector.Play();
    }

    public void SwitchConversation()
    {
        questCanvas.ConversationPanel.MessageBox.NextButton.gameObject.SetActive(true);
        conversationData = regularConversation;
    }
}
