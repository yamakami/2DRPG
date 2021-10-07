using UnityEngine;

public class Quest : MonoBehaviour
{
    public Location[] locations;

    [System.Serializable]
    public class Location
    {
        public GameObject questLocation;
        public AudioClip fieldSound;
        public bool noBattle;
        public LivingMonsterList[] monsterArea;
    }
}
