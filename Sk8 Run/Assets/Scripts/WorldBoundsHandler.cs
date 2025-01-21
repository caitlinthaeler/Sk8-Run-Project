using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsHandler : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    private Bounds worldBounds;

    public Bounds WorldBounds { get => worldBounds; }

    public Bounds GetCameraBounds(float height, float width)
    {
        SetCameraBounds(height, width);
        return worldBounds;
    }

    private void SetCameraBounds(float height, float width)
    {
        Vector3 minBound = new Vector3(boxCollider.bounds.min.x + (width / 2), boxCollider.bounds.min.y + (height / 2), -10);
        Vector3 maxBound = new Vector3(boxCollider.bounds.max.x - (width / 2), boxCollider.bounds.max.y - (height / 2), -10);
        worldBounds.SetMinMax(minBound, maxBound);
    }
}
