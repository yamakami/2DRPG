using UnityEngine;

[CreateAssetMenu(fileName = "New StartPosition", menuName = "StartPosition")]
public class StartPosition : ScriptableObject
{
    public string sceneName;
    public int locationIndex;
    public Vector2 startPosition;
    public Vector2 facingTo;
    public AudioClip audioClip;
}
