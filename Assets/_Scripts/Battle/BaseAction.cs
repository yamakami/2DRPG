using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public string characterName;
    [SerializeField] Command selectedCommand;
    [SerializeField] protected Animator enemyAttackAnim;
    [SerializeField] protected AudioClip damageClip;
    [SerializeField] protected AudioClip deadClip;
    [SerializeField] protected AudioClip missAttackClip;

    public int maxHP;
    public int maxMP;
    public int hp;
    public int attack;
    public int defence;
    public int mp;
    public int exp;
    public int gold;
    public float speed;
    public float luck;

    bool tweenPlaying;

    public Command SelectedCommand { get => selectedCommand; set => selectedCommand = value; }
    public bool TweenPlaying { get => tweenPlaying; set => tweenPlaying = value; }

    public abstract void PlayDamage(AudioSource audio);


    virtual public void PlayEnemyAttack(AudioSource audio, Command command)
    {
        enemyAttackAnim.Play(command.commandName);
        audio.PlayOneShot(command.audioClip);
    }

    public bool AnimationPlaying()
    {
        if (!enemyAttackAnim.GetCurrentAnimatorStateInfo(0).IsName("not_playing")) return true;
        return false;
    }
 
    protected void TweenEnd()
    {
        tweenPlaying = false;
    }

    public int Attack(BaseAction defender)
    {
        var attackValue = CalculateAttackValue();
        var damage = CalculateDamage(attackValue, defender);
        
        return damage;
    }

    public void AttackNoDamage(AudioSource audio)
    {
        audio.PlayOneShot(missAttackClip);
    }

    public void GenerteLuckAndSpeed()
    {
        luck = 1 + Random.Range(0.0f, 0.5f);
        speed = (attack + defence) * luck;
    }

    int CalculateAttackValue()
    {
        return (int) System.Math.Round(attack * luck, System.MidpointRounding.AwayFromZero);
    }

    int CalculateDamage(int attackValue,  BaseAction defender)
    {
        var damage = attackValue - defender.defence;

        if (0 < damage)
            return damage;

        var zeroValue = new int[] { 0, 0, 1, 2, 3 };
        var index = Random.Range(0, zeroValue.Length);

        return zeroValue[index];
    }
}
