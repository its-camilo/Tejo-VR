using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool isTransitioning = false;

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
        SceneManager.LoadScene(sceneIndex);
    }

    private bool IsValidSceneIndex(int sceneIndex)
    {
        return sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings;
    }
}
