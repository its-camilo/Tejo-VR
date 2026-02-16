using System.Collections;
using UnityEngine;

public class TejoDetector : MonoBehaviour
{
    [SerializeField] private AudioSource mechaSound;
    
    // Campos para arrastrar los 3 sistemas de partículas desde el Inspector
    [SerializeField] private ParticleSystem smoke1;
    [SerializeField] private ParticleSystem smoke2;
    [SerializeField] private ParticleSystem smoke3;
    [SerializeField] private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barro"))
        {
            Debug.Log("barro tocado");
            gameManager.AddScore(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto tiene el Tag "Mecha"
        if (other.CompareTag("Mecha"))
        {
            // Reproducir sonido si existe
            if (mechaSound != null)
            {
                mechaSound.Play();
            }

            // Obtenemos el nombre del objeto para saber cuál smoke activar
            string nombreMecha = other.gameObject.name;

            // Lógica para activar el humo correspondiente según el nombre del objeto
            if (nombreMecha == "mecha1")
            {
                smoke1.Play();
            }
            else if (nombreMecha == "mecha2")
            {
                smoke2.Play();
            }
            else if (nombreMecha == "mecha3")
            {
                smoke3.Play();
            }

            Debug.Log("Mecha tocada: " + nombreMecha);
            //+3 puntos
            gameManager.AddScore(3);
            
            Destroy(other.gameObject);
        }
    }
}
