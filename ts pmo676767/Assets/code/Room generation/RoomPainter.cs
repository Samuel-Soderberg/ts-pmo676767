using System.Globalization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class RoomPainter : MonoBehaviour
{
    [Header("Rightclick script in inspector then click PaintRoom to save")]
    [Space]
    public RoomData RoomToPaint;

    [Space]
    [Header("ignore")]
    public Tilemap PaintedTilemap;
    public Tilemap ObjectTilemap;
    public TileType floortile;
    public TileType walltile;
    public TileType emptytile;

    private DoorDirection direction;

    [ContextMenu("PaintRoom")]
    public void PaintRoom()
    {
        RoomToPaint.objectSpawns.Clear();
        int width = PaintedTilemap.cellBounds.xMax;
        int height = PaintedTilemap.cellBounds.yMax;
        //objects
        int objectcount = 0;
        foreach (var pos in PaintedTilemap.cellBounds.allPositionsWithin)
        {
            var tile = ObjectTilemap.GetTile(pos) as ObjectMarkerTile;
            if (tile != null)
            {
                RoomToPaint.objectSpawns.Add(new ObjectSpawn { marker = tile.marker, tilePosition = new Vector2Int(pos.x, pos.y) });
                objectcount++;
            } 
        }
        // tiles
        RoomToPaint.doors.Clear();
        RoomToPaint.width = width;
        RoomToPaint.height = height;
        System.Array.Resize(ref RoomToPaint.tiles, (height * width));
        for (int y = 0; y < RoomToPaint.height; y++)
        {
            for (int x = 0; x < RoomToPaint.width; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = PaintedTilemap.GetTile(pos);
                if (tile == null) RoomToPaint.SetTile(x, y, emptytile);
                else if (tile.name.Contains(floortile.name)) RoomToPaint.SetTile(x, y, floortile);
                else if (tile.name.Contains(walltile.name)) RoomToPaint.SetTile(x, y, walltile);
                else if (tile.name.Contains("Door"))
                {
                    RoomToPaint.SetTile(x, y, floortile);
                    Vector3Int below = new Vector3Int(x, y - 1, 0);
                    Vector3Int above = new Vector3Int(x, y + 1, 0);
                    Vector3Int left = new Vector3Int(x - 1, y, 0);
                    Vector3Int right = new Vector3Int(x + 1, y, 0);
                    int nullcount = 0;
                    if (PaintedTilemap.GetTile(below) == null) nullcount++;
                    if (PaintedTilemap.GetTile(above) == null) nullcount++;
                    if (PaintedTilemap.GetTile(left) == null) nullcount++;
                    if (PaintedTilemap.GetTile(right) == null) nullcount++;
                    if (nullcount == 1)
                    {
                        if (PaintedTilemap.GetTile(below) == null) direction = DoorDirection.South;
                        if (PaintedTilemap.GetTile(above) == null) direction = DoorDirection.North;
                        if (PaintedTilemap.GetTile(left) == null) direction = DoorDirection.West;
                        if (PaintedTilemap.GetTile(right) == null) direction = DoorDirection.East;
                        RoomToPaint.doors.Add(new Door { direction = direction, position = new Vector2Int(x, y) });
                    }
                    else
                    {
                        Debug.Log("room is invalid! Check doors");
                        return;
                    }
                }
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(RoomToPaint);
        AssetDatabase.SaveAssets();
#endif
        Debug.Log($"Room succesfully painted from Tilemap! With {objectcount} Gameobjects");
    }
}