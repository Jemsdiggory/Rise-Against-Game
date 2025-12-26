using UnityEngine;

public class CameraLimit2D : MonoBehaviour
{
    public Transform target; // player
    public Transform cameraTransform; // main cam
    public PolygonCollider2D mapCollider; // collider mapnya

    public float cameraZ = -10f;

    private Vector2 minLimit;
    private Vector2 maxLimit;

    void Start()
    {
        if (mapCollider != null)
        {
            Bounds bounds = mapCollider.bounds;
            minLimit = bounds.min;
            maxLimit = bounds.max;
        }
    }

    void LateUpdate()
    {
        if (target == null || cameraTransform == null) return;

        Vector3 targetPos = target.position;

        float clampedX = Mathf.Clamp(targetPos.x, minLimit.x, maxLimit.x);
        float clampedY = Mathf.Clamp(targetPos.y, minLimit.y, maxLimit.y);

        cameraTransform.position = new Vector3(clampedX, clampedY, cameraZ);
    }
}
