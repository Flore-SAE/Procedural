using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Grid))]
public class LevelGenerator : MonoBehaviour
{
    public Vector2Int gridSize;
    public bool autoWeight;
    public Cell[] baseCells;

    private Grid grid;

#if UNITY_EDITOR
    private Cell[] baseCellsCopy = Array.Empty<Cell>();
#endif

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        gridSize.Clamp(Vector2Int.zero, Vector2Int.one * int.MaxValue);

        AutoWeight();
    }

    private void AutoWeight()
    {
        if (!autoWeight)
            return;

        if (baseCellsCopy.Length == baseCells.Length)
            WeightValues();
        else
            AddOrRemoveWeightedValue();

        baseCellsCopy = new Cell[baseCells.Length];
        Array.Copy(baseCells, baseCellsCopy, baseCells.Length);
    }

    private void WeightValues()
    {
        if (!HasCellBeenModified(out var modifiedIndex)) return;

        var oldTotalLeft = 1 - baseCellsCopy[modifiedIndex].chanceToAppear;
        var newTotalLeft = 1 - baseCells[modifiedIndex].chanceToAppear;

        for (var i = 0; i < baseCells.Length; i++)
        {
            if (i == modifiedIndex) continue;

            if (oldTotalLeft != 0 || baseCellsCopy[i].chanceToAppear != 0)
            {
                var percent = baseCellsCopy[i].chanceToAppear / oldTotalLeft;
                baseCells[i].chanceToAppear = newTotalLeft * percent;
            }
            else
            {
                baseCells[i].chanceToAppear = newTotalLeft / (baseCells.Length - 1);
            }
        }
    }

    private bool HasCellBeenModified(out int modifiedIndex)
    {
        modifiedIndex = -1;

        for (var i = 0; i < baseCells.Length; i++)
        {
            var chancesDiff = baseCells[i].chanceToAppear - baseCellsCopy[i].chanceToAppear;
            if (Math.Abs(chancesDiff) <= float.Epsilon) continue;
            modifiedIndex = i;
            return true;
        }

        return false;
    }

    private void AddOrRemoveWeightedValue()
    {
        for (var i = 0; i < baseCells.Length; i++)
        {
            baseCells[i].chanceToAppear = 1f / baseCells.Length;
        }
    }
#endif

    // Start is called before the first frame update
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
                if (!(Random.value < baseCells[0].chanceToAppear)) continue;
                var cellSize = grid.cellSize;
                var indexedPosition = new Vector3(i * cellSize.x, j * cellSize.y, 0);
                var gridPosition = grid.WorldToCell(indexedPosition);
                var spawnPosition = grid.GetCellCenterWorld(gridPosition);
                var cell = Instantiate(baseCells[0].prefab, spawnPosition, Quaternion.identity, grid.transform);
                cell.transform.localScale = grid.cellSize;
            }
        }
    }
}
