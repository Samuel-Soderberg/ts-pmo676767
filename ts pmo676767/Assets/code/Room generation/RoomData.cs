using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection { North, South, East, West }

[System.Serializable]
public class Door
{
    public DoorDirection direction;
    public Vector2Int position;
}
[System.Serializable]
public struct ObjectSpawn
{
    public RoomObject marker;
    public Vector2Int tilePosition;
}

[CreateAssetMenu(menuName = "Rooms/Room Data")]
public class RoomData : ScriptableObject
{

    public int width;
    public int height;

    public TileType[] tiles;
    public List<Door> doors;

    public List<ObjectSpawn> objectSpawns = new List<ObjectSpawn>();

    TileCategory? below;
    TileCategory? above;
    public TileType GetTile(int x, int y)
    {
        int layout = y * width + x;
        return tiles[layout];
    }
    public void SetTile(int x, int y, TileType tile)
    {
        tiles[y * width + x] = tile;
    }
    public bool IsInsideRoom(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
    public WallOrientation GetWallOrientation(int x, int y)
    {
        TileType current = GetTile(x, y);

        if (current.category != TileCategory.Wall)
        {
            return WallOrientation.None;
        }

        if (IsInsideRoom(x, y + 1))
            above = GetTile(x, y + 1).category;
        if (IsInsideRoom(x, y - 1))
            below = GetTile(x, y - 1).category;

        if (below != null && below == TileCategory.Floor)
            return WallOrientation.Top;
        if (above != null && above == TileCategory.Floor)
            return WallOrientation.Bottom;
        if (below == TileCategory.Wall && above == TileCategory.Wall)
            return WallOrientation.Side;
        return WallOrientation.Side;
    }
    public WallOrientation GetWallOrientationRotated(RoomData room,int x,int y,int rotation)
    {
        if (room.GetTile(x, y).category != TileCategory.Wall)
            return WallOrientation.None;

        Vector2Int up = RoomMath.RotateDirection(Vector2Int.up, rotation);
        Vector2Int down = RoomMath.RotateDirection(Vector2Int.down, rotation);

        bool floorAbove =
            room.IsInsideRoom(x + up.x, y + up.y) &&
            room.GetTile(x + up.x, y + up.y).category == TileCategory.Floor;

        bool floorBelow =
            room.IsInsideRoom(x + down.x, y + down.y) &&
            room.GetTile(x + down.x, y + down.y).category == TileCategory.Floor;

        if (floorBelow)
            return WallOrientation.Top;

        if (floorAbove)
            return WallOrientation.Bottom;

        return WallOrientation.Side;
    }
}
