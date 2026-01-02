using UnityEngine;

public class RestrictArea : MonoBehaviour
{
    // Límites para el eje X
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    // Límites para el eje Y
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Límites para el eje Z
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    void LateUpdate()
    {
        // Obtenemos la posición actual
        Vector3 currentPosition = transform.position;

        // Aplicamos el Clamp en cada eje
        float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);
        float clampedZ = Mathf.Clamp(currentPosition.z, minZ, maxZ);

        // Asignamos la nueva posición restringida
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }
}