using UnityEngine;

[CreateAssetMenu(menuName = "Roomobject/ New RoomObject")]
public class RoomObject : ScriptableObject
{
        public GameObject prefab;
        public bool rotateWithRoom = true;
}
