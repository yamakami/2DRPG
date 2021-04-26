using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    [SerializeField] Fader fader = default;
    [SerializeField] AudioClip audioClip = default;
    [SerializeField] float audioVolume = 0.5f;
    [SerializeField] GameObject questAt = default;
    [SerializeField] GameObject locationFrom = default;
    [SerializeField] GameObject locationTo = default;
    [SerializeField] Vector2 facingTo = default;
    [SerializeField] GameObject startPosition = default;

    PlayerMove playerMove;

    public GameObject LocationFrom { get => locationFrom; }
    public GameObject LocationTo { get => locationTo; }
    public Vector2 FacingTo { get => facingTo; }
    public GameObject StartPosition { get => startPosition; }
    public AudioClip AudioClip { get => audioClip; }
    public float AudioVolume { get => audioVolume; }
    public Fader Fader { get => fader; }
    public GameObject QuestAt { get => questAt; }
    public PlayerMove PlayerMove { get => playerMove; set => playerMove = value; }
}
