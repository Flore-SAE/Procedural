using UnityEngine;

[System.Serializable]
public struct Cell
{
    public GameObject prefab;
    [Range(0, 1)] public float chanceToAppear;
}
