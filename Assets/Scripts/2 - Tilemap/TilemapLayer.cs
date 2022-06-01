using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TilemapLayer
{
    public Tilemap tilemap;
    public TilemapBuilder[] builders;

    private List<Vector3Int> positions = new List<Vector3Int>();
    private List<TileBase> tilesToPlace = new List<TileBase>();

    public void AddPosition(Vector3Int newPosition)
    {
        if (!positions.Contains(newPosition))
            positions.Add(newPosition);
    }

    public void BuildTilemap()
    {
        foreach (var builder in builders)
        {
            foreach (var position in positions)
            {
                var newTile = builder.PlaceTile(position, tilemap);
                if (newTile != null)
                    tilesToPlace.Add(newTile);
            }
        }

        tilemap.SetTiles(positions.ToArray(), tilesToPlace.ToArray());
    }
}
