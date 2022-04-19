using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class LevelGenerator : MonoBehaviour
{
    public Vector2Int gridSize;

    private LevelBuilder[] builders;
    private Grid grid;

    private void OnValidate()
    {
        gridSize.Clamp(Vector2Int.zero, Vector2Int.one * int.MaxValue);
    }

    private void Awake()
    {
        grid = GetComponent<Grid>();
        builders = GetComponents<LevelBuilder>();
        builders = builders.OrderBy(builder => builder.order).ToArray();
    }

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        for (var i = 0; i < gridSize.x; i++)
        {
            for (var j = 0; j < gridSize.y; j++)
            {
                var cellSize = grid.cellSize;
                var cellGap = grid.cellGap;
                var indexedPosition = new Vector3(i * cellSize.x + cellGap.x * i, j * cellSize.y + cellGap.y * j, 0);
                var gridPosition = grid.WorldToCell(indexedPosition);
                var spawnPosition = grid.CellToWorld(gridPosition);
                // Incompatible avec les cell gap
                //var spawnPosition = grid.GetCellCenterWorld(gridPosition);

                foreach (var builder in builders)
                {
                    var cell = builder.BuildCell(spawnPosition);
                    if (cell is null)
                        continue;
                    if (builder.shouldResize)
                        cell.transform.localScale = grid.cellSize;
                }
            }
        }
    }
}
