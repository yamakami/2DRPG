using UnityEngine;

public class LocationMove : MonoBehaviour
{
    [SerializeField] float moveTo;


    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.transform.CompareTag("Player"))
        {
            var player =  QuestManager.GetQuestManager().Player;
            player.StopMove();



        }
    }
}
