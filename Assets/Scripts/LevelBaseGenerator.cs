using UnityEngine;

public partial class LevelBaseGenerator : LevelBuilder
{
    public Cell[] baseCells = {};

    public override GameObject BuildCell(Vector3 position)
    {
        return Instantiate(GetRandomCellPrefab(), position, Quaternion.identity, transform);
    }

    private GameObject GetRandomCellPrefab()
    {
        var cell = GetRandomCell();
        return cell.prefab;
    }

    private Cell GetRandomCell()
    {
        var value = Random.value;
        var high = 0f;
        foreach (var cell in baseCells)
        {
            var low = high;
            high += cell.chanceToAppear;
            if (value > low && value < high)
                return cell;
        }

        return baseCells[0];
    }
}
