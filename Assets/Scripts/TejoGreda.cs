using UnityEngine;

public class TejoGreda : MonoBehaviour
{
    private Rigidbody rb;
    private bool estaClavado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        estaClavado = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
}