using UnityEngine;

public class Quest : MonoBehaviour
{
    public int defaultLocationNo = 0;
    public int defaultMonsterAreaNo = 0;
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
