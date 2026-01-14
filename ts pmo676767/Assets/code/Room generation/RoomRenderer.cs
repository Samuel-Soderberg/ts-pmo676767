using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.UI.Image;

public class RoomRenderer : MonoBehaviour
{
    
    public Tilemap floorTilemap;
    public RoomData startroom;
    public Tilemap wallTilemap;
    public TileTheme[] tileThemes;
    [SerializeField]
    private TileType floor;

    private Dictionary<(TileType, WallOrientation), TileBase> lookup;

    void Awake()
    {
        lookup = new Dictionary<(TileType, WallOrientation), TileBase>();
        foreach (var theme in tileThemes)
        {
            lookup[(theme.tileType, theme.orientation)] = theme.tile;
        }
        
        RoomInstance r = new RoomInstance
        {
            room = startroom,
            roomGridPosition = Vector2Int.zero,
            rotation = 0
        };
        //DrawRoom(r);
    }

    public void Closedoor(Doorsinworld door)
    {
        if (door.Door.direction == DoorDirection.South)
        {
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0) + new Vector3Int(1,0), lookup[(floor, WallOrientation.Side)]); 
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y,0), lookup[(floor, WallOrientation.Side)]); 
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0) + new Vector3Int(-1,0), lookup[(floor, WallOrientation.Side)]); 
        }
        if (door.Door.direction == DoorDirection.North)
        {
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0) + new Vector3Int(1, 0), lookup[(floor, WallOrientation.Top)]);
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0), lookup[(floor, WallOrientation.Top)]);
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0) + new Vector3Int(-1, 0), lookup[(floor, WallOrientation.Top)]);
        }
        if (door.Door.direction == DoorDirection.West || door.Door.direction == DoorDirection.East)
        {
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0) + new Vector3Int(0, 1), lookup[(floor, WallOrientation.Side)]);
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0), lookup[(floor, WallOrientation.Side)]);
            wallTilemap.SetTile(new Vector3Int(door.doorpos.x, door.doorpos.y, 0) + new Vector3Int(0, -1), lookup[(floor, WallOrientation.Side)]);
        }
    }



    public bool DrawRoom(RoomInstance roominst)
    {
       

        Vector2Int origin = roominst.roomGridPosition;
        List<Vector3Int> placedCells = new List<Vector3Int>();
        for (int y = 0; y < roominst.room.height; y++)
        {
            for (int x = 0; x < roominst.room.width; x++)
            {
                TileType tile = roominst.room.GetTile(x, y);
                if (tile.category == TileCategory.Empty)
                    continue;

                Vector2Int rotated =
                    RoomMath.RotateTile(
                        new Vector2Int(x, y),
                        roominst.room.width,
                        roominst.room.height,
                        roominst.rotation
                    );

                Vector3Int cell = new Vector3Int(
                    origin.x + rotated.x,
                    origin.y + rotated.y,
                    0
                );

                TileBase floorTile = floorTilemap.GetTile(cell);
                if (floorTile != null && floorTile.name.Contains(tile.category.ToString()))
                {
                    foreach (var posi in placedCells) { floorTilemap.SetTile(posi, null); }
                    foreach (var posi in placedCells) { wallTilemap.SetTile(posi, null); }
                    return false;
                }
                TileBase wallTile = wallTilemap.GetTile(cell);
                if (wallTile != null && wallTile.name.Contains(tile.category.ToString()))
                {
                    foreach (var posi in placedCells) { floorTilemap.SetTile(posi, null); }
                    foreach (var posi in placedCells) { wallTilemap.SetTile(posi, null); }
                    return false;
                }

                TileBase tilehopemmpty = wallTilemap.GetTile(cell);
                if (tilehopemmpty == floorTilemap.GetTile(cell))
                {
                    if (tile.category == TileCategory.Floor)
                    {
                        floorTilemap.SetTile(
                            cell,
                            lookup[(tile, WallOrientation.None)]
                        );
                        placedCells.Add(cell);
                    }
                    else if (tile.category == TileCategory.Wall)
                    {
                        WallOrientation orientation =
                            roominst.room.GetWallOrientationRotated(
                                roominst.room,
                                x,
                                y,
                                roominst.rotation
                            );

                        wallTilemap.SetTile(
                            cell,
                            lookup[(tile, orientation)]
                        );
                        placedCells.Add(cell);
                    }
                }
                else
                {
                    if (tile.category == TileCategory.Floor)
                    {
                        floorTilemap.SetTile(
                            cell,
                            lookup[(tile, WallOrientation.None)]
                        );
                    }
                    else if (tile.category == TileCategory.Wall)
                    {
                        WallOrientation orientation =
                            roominst.room.GetWallOrientationRotated(
                                roominst.room,
                                x,
                                y,
                                roominst.rotation
                            );

                        wallTilemap.SetTile(
                            cell,
                            lookup[(tile, orientation)]
                        );
                    }
                }
            }
        }
        // ---------- OBJECTS ----------
        foreach (var spawn in roominst.room.objectSpawns)
        {
            Vector2Int rotated =
                RoomMath.RotateTile(
                    spawn.tilePosition,
                    roominst.room.width,
                    roominst.room.height,
                    roominst.rotation
                );

            Vector3Int cell = new Vector3Int(
                origin.x + rotated.x,
                origin.y + rotated.y,
                0
            );

            Vector3 worldPos = floorTilemap.CellToWorld(cell);
            Instantiate(spawn.marker.prefab, worldPos, Quaternion.identity);
        }
        return true;
    }
}
