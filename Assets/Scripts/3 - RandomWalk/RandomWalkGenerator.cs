using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkGenerator : MonoBehaviour
{
    public SpriteRenderer prefab;
    public int walkerCount;
    public int stepPerWalker;
    public bool selfAvoid;

    private Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        RandomWalk();
    }

    private void RandomWalk()
    {
        var nodes = new List<Node>();
        var startNode = new Node(Vector2Int.zero, Color.white);
        nodes.Add(startNode);
        for (var i = 0; i < walkerCount; i++)
        {
            OneWalk(ref nodes);
        }
        
        foreach (var node in nodes)
        {
            var realPos = grid.CellToWorld((Vector3Int)node.position);
            var room = Instantiate(prefab, realPos, Quaternion.identity);
            room.color = node.color;
        }
    }

    private void OneWalk(ref List<Node> nodes)
    {
        var startNode = nodes[0];
        if (startNode.directions.Count <= 0)
            return;
        var position = Directions.GetRandomDirection(startNode.directions);
        var lastPosition = Vector2Int.zero;
        var lastDirection = position;
        var color = Random.ColorHSV(0, 1, 0, 1, 1, 1, 1, 1);
        for (var i = 0; i < stepPerWalker; i++)
        {
            var currentNode = GetNode(nodes, position);
            if (currentNode != null && selfAvoid)
            {
                var lastNode = GetNode(nodes, lastPosition);
                lastNode.directions.Remove(lastDirection);
                if (lastNode.directions.Count == 0)
                    break;
                var newDirection = Directions.GetRandomDirection(lastNode.directions);
                position = lastPosition + newDirection;
                lastDirection = newDirection;
                i--;
            }
            else
            {
                var newNode = new Node(position, color);
                nodes.Add(newNode);
                var newDirection = Directions.GetRandomDirection();
                lastPosition = position;
                lastDirection = newDirection;
                position += newDirection;
            }
        }
    }
    
    private Node GetNode(List<Node> nodes, Vector2Int position)
    {
        return nodes.FirstOrDefault(node => node.position == position);
    }
}
