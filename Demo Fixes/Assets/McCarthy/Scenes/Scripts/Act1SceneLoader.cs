using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is used with the flowchart from the Act 1 Scene. When the flowchart
/// reached its' last node, it activates the object holding this script and transitions
/// to the next scene.
/// </summary>
public class Act1SceneLoader : MonoBehaviour
{
    [SerializeField] private string nextScene = "";
    
    private void Start() {
        SceneManager.LoadScene(nextScene);
    }
}
