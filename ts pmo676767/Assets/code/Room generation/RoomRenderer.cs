using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.Image;

public class RoomRenderer : MonoBehaviour
{
    public RoomData room;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public TileTheme[] tileThemes;
    private Dictionary<(TileType, WallOrientation), TileBase> lookup;

    void Awake()
    {
        lookup = new Dictionary<(TileType, WallOrientation), TileBase>();
        foreach (var theme in tileThemes)
        {
            lookup[(theme.tileType, theme.orientation)] = theme.tile;
        }
        RoomInstance inst = new RoomInstance
        {
            room = room,
            roomGridPosition = Vector2Int.zero,
            rotation = 90
        };
        DrawRoom(inst, new Vector3Int(0,0,0));
    }
    public void DrawRoom(RoomInstance roominst, Vector3Int origin)
    {
        Vector3Int roomOrigin = new Vector3Int(
            roominst.roomGridPosition.x * roominst.room.width,
            roominst.roomGridPosition.y * roominst.room.height,
            0
        );
        //objects
        foreach (var spawn in roominst.room.objectSpawns)
        {
            Vector2Int rotated =
                    RoomMath.RotateTile(
                        new Vector2Int(spawn.tilePosition.x, spawn.tilePosition.y),
                        roominst.room.width,
                        roominst.room.height,
                        roominst.rotation
                    );
            Vector3Int cell = new Vector3Int(rotated.x, rotated.y, 0);
            Vector3 worldPos = floorTilemap.CellToWorld(cell);
            Instantiate(spawn.marker.prefab, floorTilemap.CellToWorld(cell), Quaternion.identity);
        }
        //tiles
        for (int y = 0; y < roominst.room.height; y++)
        {
            for (int x = 0; x < roominst.room.width; x++)
            {
                Vector2Int rotated =
                    RoomMath.RotateTile(
                        new Vector2Int(x, y),
                        roominst.room.width,
                        roominst.room.height,
                        roominst.rotation
                    );
                TileType tile = roominst.room.GetTile(x, y);
#pragma warning disable CS0642
                if (tile.category == TileCategory.Empty) ;
#pragma warning restore CS0642
                else if (tile.category == TileCategory.Floor) floorTilemap.SetTile(new Vector3Int(rotated.x, rotated.y, 0), lookup[(tile, WallOrientation.None)]);
                else
                {
                    WallOrientation orientation = roominst.room.GetWallOrientationRotated(roominst.room, x, y, roominst.rotation);
                    wallTilemap.SetTile(new Vector3Int(rotated.x, rotated.y, 0), lookup[(tile, orientation)]);
                }
            }
        }
    }
}