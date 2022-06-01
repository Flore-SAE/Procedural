using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Builders/Perlin Tile", fileName = "Perlin Tile Builder")]
public class PerlinRuleTileBuilder : TilemapBuilder
{
    [Header("Perlin properties")] 
    public TileBase ruleTile;
    public float zoom;
    [Range(0, 1)] public float threshold;

    protected override bool ShouldPlaceTile(Vector3Int position, Tilemap tilemap, out TileBase tile)
    {
        var realPos = tilemap.CellToWorld(position);
        var zoomedPosX = realPos.x / zoom;
        var zoomedPosY = realPos.y / zoom;
        var perlin = Mathf.PerlinNoise(zoomedPosX, zoomedPosY) > threshold;
        tile = perlin ? ruleTile : null;
        return perlin;
    }
}
