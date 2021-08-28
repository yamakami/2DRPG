using UnityEngine;

public class Quest : MonoBehaviour
{
    public int defaultLocationNo;
    public Location[] locations;

    [System.Serializable]
    public class Location
    {
        public int indexNo;
        public GameObject questLocation;
        public bool noBattle;
        public GameObject[] startPositions;
        public LivingMonsterList[] monsterArea;
    }
}
