using UnityEngine;

public abstract class LevelBuilderSO : ScriptableObject
{
    [Range(0, 1)] public float rate;
    public bool shouldResize;

    public bool TryBuildCell(out GameObject cell, Vector3 position, Transform parent)
    {
        if (Random.value < rate)
            return BuildCell(out cell, position, parent);
        cell = null;
        return false;
    }

    protected abstract bool BuildCell(out GameObject cell, Vector3 position, Transform parent);
}
