using UnityEngine;

public class Quest : MonoBehaviour
{
    public Location[] locations;

    [System.Serializable]
    public class Location
    {
        public GameObject questLocation;
        public bool noBattle;
        public LivingMonsterList[] monsterArea;
    }
}
