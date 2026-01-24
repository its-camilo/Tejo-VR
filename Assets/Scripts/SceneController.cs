using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeSpeed = 1.0f;

    private void Start()
    {
        // Al iniciar, si el panel está oscuro, hacemos el efecto de aclarado
        if (fadePanel != null)
        {
            StartCoroutine(FadeInRoutine());
        }
    }

    // --- PUBLIC METHODS (Para los botones) ---

    public void PlayGame()
    {
        // Carga la escena 1 (Game)
        StartCoroutine(TransitionToSceneRoutine(1));
    }

    public void PlayMenu()
    {
        // Carga la escena 0 (Menu)
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting application...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // --- PRIVATE COROUTINES (Lógica interna) ---

    // FadeIn: De Negro a Transparente
    private IEnumerator FadeInRoutine()
    {
        float alpha = 1.0f;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            UpdatePanelAlpha(alpha);
            yield return null;
        }
        fadePanel.gameObject.SetActive(false);
    }

    // FadeOut: De Transparente a Negro y cambio de escena
    private IEnumerator TransitionToSceneRoutine(int sceneIndex)
    {
        fadePanel.gameObject.SetActive(true);
        float alpha = 0.0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            UpdatePanelAlpha(alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    private void UpdatePanelAlpha(float value)
    {
        if (fadePanel != null)
        {
            Color color = fadePanel.color;
            color.a = Mathf.Clamp01(value);
            fadePanel.color = color;
        }
    }
}