using UnityEngine;

public static class RoomMath
{
    public static Vector2Int RotateTile(Vector2Int pos,int width,int height,int rotation)
    {
        switch (rotation)
        {
            case 0:
                return pos;
            case 90:
                return new Vector2Int(height - 1 - pos.y,pos.x);
            case 180:
                return new Vector2Int(width - 1 - pos.x,height - 1 - pos.y);
            case 270:
                return new Vector2Int(pos.y,width - 1 - pos.x);
            default:
                return pos;
        }
    }
    public static Vector2Int RotateDirection(Vector2Int dir,int rotation)
    {
        switch (rotation)
        {
            case 0: 
                return dir;
            case 90: 
                return new Vector2Int(dir.y, -dir.x);
            case 180: 
                return new Vector2Int(-dir.x, -dir.y);
            case 270: 
                return new Vector2Int(-dir.y, dir.x);
            default: 
                return dir;
        }
    }
}