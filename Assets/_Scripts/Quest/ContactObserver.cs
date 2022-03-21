using UnityEngine;

public class ContactObserver: MonoBehaviour
{
    private static IContactObject<NpcContact> npcContact;

    public static IContactObject<NpcContact> NpcContact { get => npcContact; set => npcContact = value; }
}
