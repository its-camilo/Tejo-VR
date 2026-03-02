using System.Collections;
using UnityEngine;

public class TejoDetector : MonoBehaviour
{
    // Audio references
    [SerializeField] private AudioSource mechaSound;
    [SerializeField] private AudioSource SonidoBarro;

    // Particle system references
    [SerializeField] private ParticleSystem smoke1;
    [SerializeField] private ParticleSystem smoke2;
    [SerializeField] private ParticleSystem smoke3;

    // Gameplay references
    [SerializeField] private GameManager gameManager;

    // Score state
    [SerializeField] private int barroPoints = 1;
    [SerializeField] private int mechaPoints = 3;

    private bool hasResolvedScore = false;
    private bool hasHitMecha = false;

    private void Awake()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is missing in TejoDetector.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barro"))
        {
            StartCoroutine(HandleBarroHitRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Mecha") || hasResolvedScore)
        {
            return;
        }

        hasHitMecha = true;
        hasResolvedScore = true;

        if (mechaSound != null)
        {
            mechaSound.Play();
        }

        string nombreMecha = other.gameObject.name;

        if (nombreMecha == "mecha1")
        {
            if (smoke1 != null)
            {
                smoke1.Play();
            }
        }
        else if (nombreMecha == "mecha2")
        {
            if (smoke2 != null)
            {
                smoke2.Play();
            }
        }
        else if (nombreMecha == "mecha3")
        {
            if (smoke3 != null)
            {
                smoke3.Play();
            }
        }

        Debug.Log("Mecha hit: " + nombreMecha);
        if (gameManager != null)
        {
            gameManager.AddScore(mechaPoints);
        }

        Destroy(other.gameObject);
    }

    private IEnumerator HandleBarroHitRoutine()
    {
        if (hasResolvedScore)
        {
            yield break;
        }

        yield return new WaitForFixedUpdate();

        if (hasResolvedScore || hasHitMecha)
        {
            yield break;
        }

        hasResolvedScore = true;
        Debug.Log("Barro hit");

        if (SonidoBarro != null && SonidoBarro.clip != null)
        {
            SonidoBarro.PlayOneShot(SonidoBarro.clip);
        }

        if (gameManager != null)
        {
            gameManager.AddScore(barroPoints);
        }
    }
}
