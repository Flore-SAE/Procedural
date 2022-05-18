using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TilemapLayer
{
    public Tilemap tilemap;
    public TilemapBuilder[] builders;

    private Dictionary<Vector3Int, TileBase> tiles = new Dictionary<Vector3Int, TileBase>();

    public void AddPosition(Vector3Int newPosition)
    {
        tiles.Add(newPosition, null);
    }

    public void BuildTilemap()
    {
        var positions = tiles.Keys.ToArray();
        foreach (var builder in builders)
        {
            foreach (var position in positions)
            {
                var newTile = builder.PlaceTile(position, tilemap);
                if (newTile != null)
                    tiles[position] = newTile;
            }
        }
        tilemap.SetTiles(tiles.Keys.ToArray(), tiles.Values.ToArray());
    }
}
