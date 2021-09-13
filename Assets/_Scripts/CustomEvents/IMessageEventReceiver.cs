using UnityEngine.EventSystems;

public interface IMessageEventReceiver : IEventSystemHandler
{
    void Receive(MesageEvent.MessageEventMethods messageEventMethods);
}
