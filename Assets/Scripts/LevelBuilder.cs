using UnityEngine;

public abstract class LevelBuilder : MonoBehaviour
{
    [Range(0, 9)] public int order;
    [Range(0, 1)] public float rate;
    public bool shouldResize;

    public abstract GameObject BuildCell(Vector3 position);
}
