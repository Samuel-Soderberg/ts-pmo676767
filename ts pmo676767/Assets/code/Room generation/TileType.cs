using UnityEngine;

public enum WallOrientation
{
    None,
    Top,
    Side,
    Bottom
}
public enum TileCategory
{
    Floor,
    Wall,
    Empty
}
[CreateAssetMenu(menuName = "Tiles/Tile Type")]
public class TileType : ScriptableObject
{
    public TileCategory category;
    public string tileName;
    //public bool walkable;
}