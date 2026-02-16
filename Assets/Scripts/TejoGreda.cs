using UnityEngine;

public class TejoGreda : MonoBehaviour
{
    // Physics state
    private Rigidbody rb;
    private bool estaClavado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing in TejoGreda.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barro") && !estaClavado)
        {
            ClavarTejo();
        }
    }

    void ClavarTejo()
    {
        if (rb == null)
        {
            return;
        }

        estaClavado = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
}