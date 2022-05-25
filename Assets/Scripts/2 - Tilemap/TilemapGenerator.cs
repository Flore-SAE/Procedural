using UnityEngine;

public class TilemapGenerator : MonoBehaviour
{
    public Vector2Int gridSize;
    public TilemapLayer[] layers;

    private Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        CreateLevel();
    }

    private void OnValidate()
    {
        gridSize.Clamp(Vector2Int.zero, Vector2Int.one * int.MaxValue);
    }
    
    private void CreateLevel()
    {
        var cellSize = grid.cellSize;
        var cellGap = grid.cellGap;
        for (var i = 0; i < gridSize.x; i++)
        for (var j = 0; j < gridSize.y; j++)
        {
            var indexedPosition = new Vector3(i * cellSize.x + cellGap.x * i, j * cellSize.y + cellGap.y * j, 0);
            var gridPosition = grid.WorldToCell(indexedPosition);

            foreach (var layer in layers)
            {
                layer.AddPosition(gridPosition);
            }
        }

        foreach (var layer in layers)
        {
            layer.BuildTilemap();
        }
    }
}
