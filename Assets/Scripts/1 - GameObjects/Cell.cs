using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable PossibleMultipleEnumeration

[System.Serializable]
public struct Cell
{
    public GameObject prefab;
    [Range(0, 1)] public float chanceToAppear;

    public static Cell GetRandomCell(IEnumerable<Cell> cells)
    {
        var value = Random.value;
        var high = 0f;
        foreach (var cell in cells)
        {
            var low = high;
            high += cell.chanceToAppear;
            if (value > low && value < high)
                return cell;
        }

        return cells.First();
    }
}
