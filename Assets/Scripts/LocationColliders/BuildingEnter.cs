using UnityEngine;

public class BuildingEnter : MonoBehaviour
{
    [SerializeField] GameObject mainGrid = default;
    [SerializeField] GameObject houseGrid = default;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            mainGrid.SetActive(false);
            houseGrid.SetActive(true);
        }
    }
}
