using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    public static List<Vector2Int> all = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public static Vector2Int GetRandomDirection()
    {
        var index = Random.Range(0, all.Count);
        return all[index];
    }

    public static Vector2Int GetRandomDirection(List<Vector2Int> directions)
    {
        var index = Random.Range(0, directions.Count);
        return directions[index];
    }
}
