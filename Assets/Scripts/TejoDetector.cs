using UnityEngine;

public class TejoDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barro"))
        {
            Debug.Log("barro tocado");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mecha"))
        {
            Debug.Log("mecha tocada");
        }
    }
}