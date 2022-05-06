using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] Canvas disableCover;
    [SerializeField] Canvas controlPanel;
    [SerializeField] Fade fade;
    [SerializeField] Conversation conversation;
    [SerializeField] Shopping shopping;


    public AudioSource MainAudioSource { get => mainAudioSource; }
    public AudioSource SeAudioSource { get => seAudioSource; }
    public Conversation Conversation { get => conversation; }
    public Shopping Shopping { get => shopping; }
    public Fade Fade { get => fade; }

    public void MenuOn(bool value)
    {
        disableCover.enabled = value;
    }

    public void ControlPanelOn(bool value)
    {
        controlPanel.enabled = value;
    }
} 
