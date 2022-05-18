using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Builders/Perlin Tile", fileName = "Perlin Tile Builder")]
public class PerlinRuleTileBuilder : TilemapBuilder 
{
    public float islandSize;
    [Range(0, 1)] public float threshold;

    protected override bool ShouldPlaceTile(Vector3Int position, Tilemap tilemap)
    {
        if (tilemap.orientation == Tilemap.Orientation.Custom)
            return false;
        
        var realPos = tilemap.CellToWorld(position);
        var tiledPosition = tilemap.orientation switch
        {
           Tilemap.Orientation.XY => new Vector2(realPos.x, realPos.y),
           Tilemap.Orientation.XZ => new Vector2(realPos.x, realPos.z),
           Tilemap.Orientation.YX => new Vector2(realPos.y, realPos.x),
           Tilemap.Orientation.YZ => new Vector2(realPos.y, realPos.z),
           Tilemap.Orientation.ZX => new Vector2(realPos.z, realPos.x),
           Tilemap.Orientation.ZY => new Vector2(realPos.z, realPos.y),
           Tilemap.Orientation.Custom => new Vector2(float.MinValue, float.MinValue),
           _ => new Vector2(float.MinValue, float.MinValue)
        };
        var zoomedPosX = tiledPosition.x / islandSize;
        var zoomedPosY = tiledPosition.y / islandSize;
        return Mathf.PerlinNoise(zoomedPosX, zoomedPosY) > threshold;
    }
}
