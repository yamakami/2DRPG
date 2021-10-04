using UnityEngine;

public class Quest : MonoBehaviour
{
    public Location[] locations;

    [System.Serializable]
    public class Location
    {
        public GameObject questLocation;
        public GameObject mainField;
        public bool noBattle;
        public LivingMonsterList[] monsterArea;
        public SavedLocation[] savedLocations;
    }

    [System.Serializable]
    public class SavedLocation
    {
        public GameObject location;
        public Vector2 startPosition;
        public Vector2 playerFacing;
        public bool NoBattle = true;
    }
}
