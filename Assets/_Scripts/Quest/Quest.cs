using UnityEngine;

public class Quest : MonoBehaviour
{
    public Location[] locations;
    public SavedLocation[] savelocations;

    [System.Serializable]
    public class Location
    {
        public GameObject questLocation;
        public AudioClip fieldSound;
        public bool noBattle;
        public LivingMonsterList[] monsterArea;
    }

    [System.Serializable]
    public class SavedLocation
    {
        public int questLocationIndex;
        public GameObject activateLocation;
        public bool activateMainLocation = true;
        public Vector2 startPosition;
        public Vector2 facingTo;
    }
}
