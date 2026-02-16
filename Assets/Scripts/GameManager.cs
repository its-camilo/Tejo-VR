using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    //referencia al texto de la ui para mostrar el puntaje, etc.

    private void Start()
    {
        // Inicialización del juego
        //el texto de la ui es el score
    }

    private void Update()
    {
        // el texto de la ui se actualiza con el puntaje actual
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Puntaje actualizado: " + score);
    }
}