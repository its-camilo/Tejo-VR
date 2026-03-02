using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TejoGreda : MonoBehaviour
{
    // Physics state
    [SerializeField] private GameManager gameManager;
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool estaClavado = false;
    private bool isHeld = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing in TejoGreda.");
        }

        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component is missing in TejoGreda.");
        }
    }

    void OnEnable()
    {
        if (grabInteractable == null)
        {
            return;
        }

        grabInteractable.selectEntered.AddListener(OnGrabStarted);
        grabInteractable.selectExited.AddListener(OnGrabEnded);
    }

    void OnDisable()
    {
        if (grabInteractable == null)
        {
            return;
        }

        grabInteractable.selectEntered.RemoveListener(OnGrabStarted);
        grabInteractable.selectExited.RemoveListener(OnGrabEnded);
    }

    private void OnGrabStarted(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    private void OnGrabEnded(SelectExitEventArgs args)
    {
        if (!isHeld)
        {
            return;
        }

        isHeld = false;

        if (gameManager != null)
        {
            gameManager.IncrementTejosLanzados();
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
