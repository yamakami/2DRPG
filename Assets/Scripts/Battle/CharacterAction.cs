using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] Animator animator = default;
    [SerializeField] StatusBar statusBar = default;

    public string characterName;    
    public int maxHP;
    public int maxMP;
    public int hp;
    public int attack;
    public int defence;
    public int mp;
    public int exp;
    public int gold;
    public float speed;

    BattleManager battleManager = default;

    public Animator Animator { get => animator; }
    public BattleManager BattleManager { get => battleManager; set => battleManager = value; }
    public StatusBar StatusBar { get => statusBar; }
}
