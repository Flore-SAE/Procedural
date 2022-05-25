using UnityEngine;

[CreateAssetMenu(menuName = "Builders/Perlin", fileName = "Perlin Builder")]
public class PerlinCellBuilder : LevelBuilder
{
    [Header("Perlin properties")]
    public GameObject prefab;
    public float zoom;
    [Range(0, 1)] public float threshold;

    protected override bool BuildCell(out GameObject cell, Vector3 position, Transform parent)
    {
        var perlinPosX = position.x / zoom;
        var perlinPosY = position.y / zoom;
        if (Mathf.PerlinNoise(perlinPosX, perlinPosY) > threshold)
        {
            cell = Instantiate(prefab, position, parent.rotation, parent);
            return true;
        }

        cell = null;
        return false;
    }
}
