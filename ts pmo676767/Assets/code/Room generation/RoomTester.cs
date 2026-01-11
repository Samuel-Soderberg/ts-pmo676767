using UnityEngine;
using System.Text;
public class RoomTester : MonoBehaviour
{
    public RoomData room;

    void Start()
    {
        PrintRoom();
    }

    void PrintRoom()
    {
        StringBuilder sb = new StringBuilder();

        for (int y = room.height - 1; y >= 0; y--)
        {
            for (int x = 0; x < room.width; x++)
            {
                TileType tile = room.GetTile(x, y);
                
                if (room.GetWallOrientation(x, y) == WallOrientation.None) sb.Append("F");
                else if (room.GetWallOrientation(x, y) == WallOrientation.Top) sb.Append("T");
                else if (room.GetWallOrientation(x, y) == WallOrientation.Side) sb.Append("S");
                else if (room.GetWallOrientation(x, y) == WallOrientation.Bottom) sb.Append("B");
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
}
