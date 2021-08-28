using UnityEngine;
using System.Threading;

public class FlowBase : MonoBehaviour
{
    protected BattleUI battleUI;
    public BattleUI BattleUI { get => battleUI; set => battleUI = value; }
    protected int delaytime = 500;
    protected CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    protected void ActivateMessageBox(BattleMessageBox messageBox)
    {
        messageBox.Activate();
        messageBox.DisplayMessage();
    }

    virtual protected void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }

    protected virtual void DisplayMessage(BattleMessageBox messageBox, string message, params object[] args)
    {
        messageBox.StringBuilder.Clear();
        messageBox.StringBuilder.AppendFormat(message, args);

        ActivateMessageBox(messageBox);
    }
}
