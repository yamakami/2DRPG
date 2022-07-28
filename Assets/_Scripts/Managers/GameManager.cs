using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] AudioClip[] buttonSounds = new AudioClip[2];

    public AudioClip[] ButtonSounds { get => buttonSounds;  }
}
