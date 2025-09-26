using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string gameplayScene = "Gameplay";
    [SerializeField] private GameObject optionsPanel;

    public void OnStart()
    {
        SceneManager.LoadScene(gameplayScene);
    }

    public void OnOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
