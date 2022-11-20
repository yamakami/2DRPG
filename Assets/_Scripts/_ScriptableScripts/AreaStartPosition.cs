using UnityEngine;

[CreateAssetMenu(fileName = "_AreaStartPosition", menuName = "StartPosition")]
public class AreaStartPosition : ScriptableObject
{
    [SerializeField] public readonly Vector2 startPosition;
}
