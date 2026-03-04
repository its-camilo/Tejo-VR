using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Score and throw settings
    private const int MaxTejos = 3;
    private const int MinScoreValue = 0;
    private const int MaxScoreValue = 10;
    private const int PerfectMechaStreakBonus = 1;

    // Game state
    private int score = 0;
    private int tejosLanzados = 0;
    private int consecutiveMechaHits = 0;

    // High score
    private const string HighScoreKey = "HighScore";
    private int highScore = 0;

    // Toggle PlayerPrefs persistence from the Inspector.
    // When disabled, highScore starts at 0 and is not persisted between sessions.
    [SerializeField] private bool playerPrefsActive = false;

    // Scene and UI references
    [SerializeField] private SceneController sceneController;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text maxScoreText;
    [SerializeField] private TMP_Text tejosLanzadosText;
    [SerializeField] private AudioSource feedback;

    private bool gameEnded = false;
    private bool lastPlayerPrefsActive;
    private bool endFeedbackLocked = false;

    public int Score => score;
    public int TejosLanzados => tejosLanzados;
    public int HighScore => highScore;

    private void Start()
    {
        if (sceneController == null)
        {
            Debug.LogError("SceneController reference is missing in GameManager.");
        }

        lastPlayerPrefsActive = playerPrefsActive;
        LoadHighScore();
        UpdateUI();
    }

    private void Update()
    {
        if (playerPrefsActive != lastPlayerPrefsActive)
        {
            lastPlayerPrefsActive = playerPrefsActive;
            LoadHighScore();
        }

        if (tejosLanzados >= MaxTejos && !gameEnded)
        {
            gameEnded = true;

            SaveHighScore();
            UpdateUI();

            Invoke(nameof(PlayEndFeedback), 1f);
        }
    }

    public void AddScore(int points)
    {
        AddScore(points, false);
    }

    public void AddScore(int points, bool isMechaHit)
    {
        score = Mathf.Clamp(score + points, MinScoreValue, MaxScoreValue);

        if (isMechaHit)
        {
            consecutiveMechaHits++;
        }
        else
        {
            consecutiveMechaHits = 0;
        }

        if (isMechaHit && consecutiveMechaHits == MaxTejos && tejosLanzados >= MaxTejos)
        {
            score = Mathf.Clamp(score + PerfectMechaStreakBonus, MinScoreValue, MaxScoreValue);
            Debug.Log("Racha perfecta de mecha: +1 punto de bonificacion.");
        }

        SaveHighScore();
        UpdateUI();
        Debug.Log("Puntaje actualizado: " + score);
    }

    public void IncrementTejosLanzados()
    {
        tejosLanzados++;
        UpdateUI();
        Debug.Log("Tejos lanzados: " + tejosLanzados);
    }

    private void LoadHighScore()
    {
        if (playerPrefsActive)
        {
            highScore = Mathf.Clamp(PlayerPrefs.GetInt(HighScoreKey, 0), MinScoreValue, MaxScoreValue);
        }
        else
        {
            highScore = MinScoreValue;
            PlayerPrefs.SetInt(HighScoreKey, MinScoreValue);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    private void SaveHighScore()
    {
        if (!playerPrefsActive)
        {
            highScore = MinScoreValue;
            UpdateUI();
            return;
        }

        if (score > highScore)
        {
            highScore = Mathf.Clamp(score, MinScoreValue, MaxScoreValue);

            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
            Debug.Log("Nuevo puntaje mas alto guardado: " + highScore);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if (maxScoreText != null)
        {
            maxScoreText.text = highScore.ToString();
        }

        if (tejosLanzadosText != null)
        {
            tejosLanzadosText.text = tejosLanzados.ToString();
        }
    }

    private void PlayEndFeedback()
    {
        if (endFeedbackLocked)
        {
            return;
        }

        endFeedbackLocked = true;

        if (feedback != null)
        {
            feedback.Play();
        }

        Invoke(nameof(UnlockEndFeedback), 1f);
        Invoke(nameof(RestartGame), 2.5f);
    }

    private void UnlockEndFeedback()
    {
        endFeedbackLocked = false;
    }

    private void RestartGame()
    {
        if (sceneController != null)
        {
            sceneController.PlayGame();
        }
    }

}
