using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] Conversation conversation;

    public AudioSource MainAudioSource { get => mainAudioSource; }

    public Conversation Conversation { get => conversation; }
}
 
