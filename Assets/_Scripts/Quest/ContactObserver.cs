using UnityEngine;

public class ContactObserver: MonoBehaviour
{
    private static IContactObject<NpcContact> npcContact;
    // private static IContactObject itemContact;

    public static IContactObject<NpcContact> NpcContact { get => npcContact; set => npcContact = value; }
    // public static IContactObject ItemContact { get => itemContact; set => itemContact = value; }
}
