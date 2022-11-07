using UnityEngine;

public  class SoundManager: MonoBehaviour
{
    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource subAudio;

    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip buttonHover;

    bool mainAudioIsoOn;
    bool subAudioIsBusy;

    public AudioSource MainAudio { get => mainAudio; }
    public AudioSource SubAudio { get => subAudio; }
    public bool MainAudioIsoOn { get => mainAudioIsoOn; set => mainAudioIsoOn = value; }
    public bool SubAudioIsBusy { get => subAudioIsBusy; set => subAudioIsBusy = value; }

    public void PlaySubAudio(AudioClip clip)
    {
        subAudio.clip = clip;
        subAudio.Play();
    }

    public void StopSubAudio()
    {
        subAudio.Stop();
        subAudio.clip = null;
    }

    public void PlayButtonClick() => subAudio.PlayOneShot(buttonClick);

    public void PlayButtonHover() => subAudio.PlayOneShot(buttonHover);
}
