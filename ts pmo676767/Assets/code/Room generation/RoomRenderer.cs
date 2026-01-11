using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    }
    public void DrawRoom()
    {
        //objects
        foreach (var spawn in room.objectSpawns)
        {
            Vector3Int cell = new Vector3Int(spawn.tilePosition.x, spawn.tilePosition.y, 0);
            Vector3 worldPos = floorTilemap.CellToWorld(cell);
            Instantiate(spawn.marker.prefab,floorTilemap.CellToWorld(cell),Quaternion.identity);
        }
        //tiles
        for (int y = 0; y < room.height; y++)
        {
            for (int x = 0; x < room.width; x++)
            {
                TileType tile = room.GetTile(x, y);
                Vector3Int pos = new Vector3Int(x, y, 0);

#pragma warning disable CS0642
                if (tile.category == TileCategory.Empty) ;
#pragma warning restore CS0642
                else if (tile.category == TileCategory.Floor) floorTilemap.SetTile(pos, lookup[(tile, WallOrientation.None)]);
                else
                {
                    WallOrientation orientation = room.GetWallOrientation(x, y);
                    wallTilemap.SetTile(pos, lookup[(tile, orientation)]);
                }
            }
        }
    }

    void Start()
    {
        DrawRoom();
    }
}