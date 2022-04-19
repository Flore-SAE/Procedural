using UnityEngine;

public class IslandGenerator : LevelBuilder
{
    public GameObject prefab;

    public override GameObject BuildCell(Vector3 position)
    {
        if (Random.value < rate)
            return Instantiate(prefab, position, Quaternion.identity, transform);
        return null;
    }
}
