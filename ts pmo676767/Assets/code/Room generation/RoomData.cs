using UnityEngine;
[CreateAssetMenu(menuName = "Rooms/Room Data")]
public class RoomData : ScriptableObject
{
    public int width;
    public int height;

    public TileType[] tiles;

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
}
