using UnityEngine;

[CreateAssetMenu(fileName = "Island Cell", menuName = "Builders/Island", order = 0)]
public class IslandBuilder : LevelBuilder
{
    public GameObject prefab;

    protected override bool BuildCell(out GameObject cell, Vector3 position, Transform parent)
    {
        if (Random.value < rate)
        {
            cell = Instantiate(prefab, position, parent.rotation, parent);
            return true;
        }

        cell = null;
        return false;
    }
}