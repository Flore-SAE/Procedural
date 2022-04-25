using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct Cell
{
    public GameObject prefab;
    [Range(0, 1)] public float chanceToAppear;

    public static Cell GetRandomCell(IEnumerable<Cell> cells)
    {
        var cellArray = cells.ToArray();
        var value = Random.value;
        var high = 0f;
        foreach (var cell in cellArray)
        {
            var low = high;
            high += cell.chanceToAppear;
            if (value > low && value < high)
                return cell;
        }

        return cellArray[0];
    }
}
