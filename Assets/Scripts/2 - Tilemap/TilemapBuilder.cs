using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TilemapBuilder : ScriptableObject
{
    [Header("Common properties")]
    public bool replace;

    public RuleTile tile;
    
    public TileBase PlaceTile(Vector3Int position, Tilemap tilemap)
    {
        if(!replace && tilemap.HasTile(position))
            return null;

        return ShouldPlaceTile(position, tilemap) ? tile : null;
    }

    protected abstract bool ShouldPlaceTile(Vector3Int position, Tilemap tilemap);
}
