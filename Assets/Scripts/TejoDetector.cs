using UnityEngine;

public class TejoDetector : MonoBehaviour
{
    [SerializeField] private AudioSource mechaSound;
    
    // Campos para arrastrar los 3 sistemas de partículas desde el Inspector
    [SerializeField] private ParticleSystem smoke1;
    [SerializeField] private ParticleSystem smoke2;
    [SerializeField] private ParticleSystem smoke3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barro"))
        {
            Debug.Log("barro tocado");
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
            // Nota: usamos .Contains por si el nombre tiene algo como "(Clone)" al final
            if (nombreMecha == "Mecha1")
            {
                if (smoke1 != null) smoke1.Emit();
            }
            else if (nombreMecha == "Mecha2")
            {
                if (smoke2 != null) smoke2.Emit();
            }
            else if (nombreMecha == "Mecha3")
            {
                if (smoke3 != null) smoke3.Emit();
            }

            Debug.Log("Mecha tocada: " + nombreMecha);
            
            // Destruir la mecha detectada
            Destroy(other.gameObject);
        }
    }
}