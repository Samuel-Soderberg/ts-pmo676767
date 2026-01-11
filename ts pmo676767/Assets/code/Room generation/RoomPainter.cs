using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomPainter : MonoBehaviour
{
    [Header("Rightclick script in inspector then click PaintRoom to save")]
    [Space]
    public RoomData RoomToPaint;

    [Space]
    [Header("ignore")]
    public Tilemap PaintedTilemap;
    public TileType floortile;
    public TileType walltile;

    [ContextMenu("PaintRoom")]
    public void PaintRoom()
    {
        RoomToPaint.width = PaintedTilemap.cellBounds.xMax;
        RoomToPaint.height = PaintedTilemap.cellBounds.yMax;
        System.Array.Resize(ref RoomToPaint.tiles, (PaintedTilemap.cellBounds.yMax * PaintedTilemap.cellBounds.xMax));
        for (int y = 0; y < RoomToPaint.height; y++)
        {
            for (int x = 0; x < RoomToPaint.width; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = PaintedTilemap.GetTile(pos);
                if (tile.name.Contains(floortile.name))
                {
                    RoomToPaint.SetTile(x, y, floortile);
                }
                else if (tile.name.Contains(walltile.name))
                {
                    RoomToPaint.SetTile(x, y, walltile);
                }
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(RoomToPaint);
        AssetDatabase.SaveAssets();
#endif
        Debug.Log("Room painted from Tilemap!");
    }
}
