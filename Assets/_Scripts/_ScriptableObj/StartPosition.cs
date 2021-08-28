using UnityEngine;

[CreateAssetMenu(fileName = "New StartPosition", menuName = "StartPosition")]
public class StartPosition : ScriptableObject
{
    public string sceneName;
    public string locationTo;
    public int startPositionIndex;
    public Vector2 facingTo;
    public AudioClip audioClip;
}
