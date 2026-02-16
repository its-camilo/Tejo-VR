using UnityEngine;

public class RestrictArea : MonoBehaviour
{
    // X-axis limits
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    // Y-axis limits
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Z-axis limits
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;

        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);
        float clampedZ = Mathf.Clamp(currentPosition.z, minZ, maxZ);

        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}