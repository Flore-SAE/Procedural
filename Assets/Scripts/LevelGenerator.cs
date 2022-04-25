using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class LevelGenerator : MonoBehaviour
{
    public Vector2Int gridSize;
    public LevelBuilderSO[] builders;

    private Grid grid;

    private void OnValidate()
    {
        gridSize.Clamp(Vector2Int.zero, Vector2Int.one * int.MaxValue);
    }

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        var cellSize = grid.cellSize;
        var cellGap = grid.cellGap;
        for (var i = 0; i < gridSize.x; i++)
        {
            for (var j = 0; j < gridSize.y; j++)
            {
                var indexedPosition = new Vector3(i * cellSize.x + cellGap.x * i, j * cellSize.y + cellGap.y * j, 0);
                var gridPosition = grid.WorldToCell(indexedPosition);
                var spawnPosition = grid.CellToWorld(gridPosition);
                // Incompatible avec les cell gap
                //var spawnPosition = grid.GetCellCenterWorld(gridPosition);

                foreach (var builder in builders)
                {
                    if (builder.TryBuildCell(out var cell, spawnPosition, transform))
                    {
                        if (builder.shouldResize)
                            cell.transform.localScale = grid.cellSize;
                    }
                }
            }
        }
    }
}
