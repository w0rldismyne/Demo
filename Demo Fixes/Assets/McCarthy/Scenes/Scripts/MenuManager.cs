using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Helper class to switch scenes.
/// </summary>
public class MenuManager : MonoBehaviour
{
    public void GoToScene(string s) {
        SceneManager.LoadScene(s);
    }
}
