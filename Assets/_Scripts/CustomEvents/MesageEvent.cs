using UnityEngine;

public class MesageEvent : MonoBehaviour, IMessageEventReceiver
{
    [SerializeField] MessageBox messageBox;

    public enum MessageEventMethods
    {
        None,
        Test1,
        Test2,
    }

    void Start()
    {
        messageBox.MessageEventReceiver = gameObject;
    }

    public void Receive(MessageEventMethods messageEventMethods)
    {

        Debug.Log("---------------------message event invoked!!!!!!");
        // イベントメソッド分岐
        //  switch(messageEventMethods)
        // {
        //     case MessageEventMethods.None:
        //         break;
        //     default:
        //         break;
        // }
    }
}
