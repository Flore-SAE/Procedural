#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;

public partial class LevelBaseGenerator
{
    private Cell[] baseCellsCopy = Array.Empty<Cell>();

    private void OnValidate()
    {
        AutoWeight();
    }

    private void AutoWeight()
    {
        if (baseCellsCopy.Length == baseCells.Length)
            WeightValues();
        else
            AddOrRemoveWeightedValue();

        baseCellsCopy = new Cell[baseCells.Length];
        Array.Copy(baseCells, baseCellsCopy, baseCells.Length);
    }

    private void WeightValues()
    {
        if (!HasCellInspectorBeenModified(out var modifiedIndex)) return;

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

            baseCells[i].chanceToAppear = Mathf.Clamp(baseCells[i].chanceToAppear, 0, 1);
        }
    }

    private bool HasCellInspectorBeenModified(out int modifiedIndex)
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
        var cellAdded = baseCells.Length > baseCellsCopy.Length;

        if (cellAdded)
        {
            baseCells[baseCells.Length - 1].chanceToAppear = 0;
        }
        else
        {
            var chanceToDivide = GetRemovedCellChance();
            chanceToDivide /= baseCells.Length;
            for (var i = 0; i < baseCells.Length; i++)
            {
                baseCells[i].chanceToAppear += chanceToDivide;
            }
        }
    }

    private float GetRemovedCellChance()
    {
        foreach (var cell in baseCellsCopy)
        {
            if (!baseCells.Contains(cell))
                return cell.chanceToAppear;
        }

        return 0;
    }
}
#endif
