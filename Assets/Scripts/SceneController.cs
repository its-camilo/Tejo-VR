using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeSpeed = 1.0f;

    private bool isTransitioning = false;

    private void Start()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeInRoutine());
        }
    }


    public void PlayGame()
    {
        TryTransitionToScene(1);
    }

    public void PlayMenu()
    {
        TryTransitionToScene(0);
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

    private IEnumerator TransitionToSceneRoutine(int sceneIndex)
    {
        if (fadePanel == null)
        {
            SceneManager.LoadScene(sceneIndex);
            yield break;
        }

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

    private void TryTransitionToScene(int sceneIndex)
    {
        if (isTransitioning)
        {
            return;
        }

        if (!IsValidSceneIndex(sceneIndex))
        {
            Debug.LogError($"Scene index {sceneIndex} is not valid.");
            return;
        }

        isTransitioning = true;

        if (fadePanel != null)
        {
            StartCoroutine(TransitionToSceneRoutine(sceneIndex));
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private bool IsValidSceneIndex(int sceneIndex)
    {
        return sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings;
    }
}