using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Vector2Int> directions;
    public Vector2Int position;
    public Color color;

    public Node(Vector2Int newPosition, Color newColor)
    {
        directions = new List<Vector2Int>(Directions.all);
        position = newPosition;
        color = newColor;
    }
}
