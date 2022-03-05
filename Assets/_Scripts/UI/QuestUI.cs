using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] Canvas disableCover;
    [SerializeField] Canvas controlPanel;
    [SerializeField] Conversation conversation;

    public AudioSource MainAudioSource { get => mainAudioSource; }
    public AudioSource SeAudioSource { get => seAudioSource; }
    public Conversation Conversation { get => conversation; }

    public void MenuOn(bool value)
    {
        disableCover.enabled = value;
    }

    public void ControlPanelOn(bool value)
    {
        controlPanel.enabled = value;
    }
} 
