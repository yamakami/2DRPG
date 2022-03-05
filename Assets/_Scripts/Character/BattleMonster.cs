using UnityEngine;

public class BattleMonster : MonoBehaviour, IStatus
{
    int hp;
    int mp;

    int maxHP;
    int maxMP;

    public string CharacterName { get => "dummy"; }
    public int HP { get => hp; set => hp = value; }
    public int MP { get => mp; set => mp = value; }

    public int MaxHP { get => maxHP; }
    public int MaxMP { get => maxMP; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
