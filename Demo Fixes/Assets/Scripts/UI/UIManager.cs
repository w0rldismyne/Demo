using UnityEngine;
using UnityEngine.SceneManagement;
using Lucerna.Utils;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;
    private bool transitioning = false;
    private void Update() {
        if (transitioning) return;
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }

    public void GoToScene(string sceneName) {
        try { transitioning = true; Time.timeScale = 1f; SceneLoader.instance.LoadSceneAsync(sceneName); }
        catch { SceneManager.LoadScene(sceneName); }
    }
}
