using UnityEngine;

[CreateAssetMenu(fileName = "Base Cells", menuName = "Builders/Base", order = 0)]
public partial class BaseCellBuilder : LevelBuilderSO
{
    public Cell[] baseCells = { };

    protected override bool BuildCell(out GameObject cell, Vector3 position, Transform parent)
    {
        var cellConfig = Cell.GetRandomCell(baseCells);
        cell = Instantiate(cellConfig.prefab, position, parent.rotation, parent);
        return true;
    }
}
