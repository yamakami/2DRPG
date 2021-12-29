using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] MessageBox messageBox;
    public AudioSource AudioSource { get => audioSource; }
    public MessageBox MessageBox { get => messageBox; }
}
