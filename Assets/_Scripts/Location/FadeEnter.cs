using UnityEngine;

public class FadeEnter : MonoBehaviour
{
    [SerializeField] StartPosition startPosition;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.QuestManager.QuestUI.LocationManager.FadeAndChangeLocation(startPosition);
        }
    }
}
