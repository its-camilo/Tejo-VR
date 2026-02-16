using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Score and throw settings
    private const int MaxTejos = 3;

    // Game state
    private int score = 0;
    private int tejosLanzados = 0;

    // High score
    private const string HighScoreKey = "HighScore";
    private int highScore = 0;

    // Toggle PlayerPrefs persistence from the Inspector.
    // When disabled, all PlayerPrefs code is skipped and highScore stays at 0.
    [SerializeField] private bool playerPrefsActive = false;

    // Scene references
    [SerializeField] private SceneController sceneController;

    private bool gameEnded = false;

    public int Score => score;
    public int TejosLanzados => tejosLanzados;
    public int HighScore => highScore;

    private void Start()
    {
        if (sceneController == null)
        {
            Debug.LogError("SceneController reference is missing in GameManager.");
        }

        LoadHighScore();

        // TODO: Vincular los valores de puntaje y lanzamientos a elementos de texto de la UI.
        // TODO: Crear UI persistente en la escena Game que muestre el puntaje más alto (highScore).
    }

    private void Update()
    {
        if (tejosLanzados >= MaxTejos && !gameEnded)
        {
            gameEnded = true;
            tejosLanzados = 0;

            SaveHighScore();

            // TODO: Mostrar retroalimentación de fin de juego y puntaje final antes de salir de la escena.
            if (sceneController != null)
            {
                sceneController.PlayMenu();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Puntaje actualizado: " + score);
    }

    public void IncrementTejosLanzados()
    {
        tejosLanzados++;
        Debug.Log("Tejos lanzados: " + tejosLanzados);
    }

    private void LoadHighScore()
    {
        if (playerPrefsActive)
        {
            highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt(HighScoreKey, 0);
            PlayerPrefs.Save();
        }
    }

    private void SaveHighScore()
    {
        if (!playerPrefsActive) return;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
            Debug.Log("Nuevo puntaje más alto guardado: " + highScore);
        }
    }
}