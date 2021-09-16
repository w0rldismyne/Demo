using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class deals with which scene to trigger when the boss gets to half health.
/// </summary>
public class EnemyInteraction : MonoBehaviour
{
    // VARIABLES
    private BattleSystem battleSystem;
    [SerializeField] private string heroScene = "";
    [SerializeField] private string villainScene = "";

    private bool doneHalfHp = false;

    private float alignmentPoints;

    // EXECUTION FUNCTIONS
    private void Start() {
        battleSystem = FindObjectOfType<BattleSystem>();
        // Right now, this is just using a random number,
        // You would use your alignment points instead if "Random.Range(0, 100)".
        alignmentPoints = Random.Range(0, 100);
    }

    private void Update() {
        if (battleSystem.Boss.currentHP <= (battleSystem.Boss.maxHP / 2) && !doneHalfHp) {
            TriggerHalfHealth();
        }
    }

    // METHODS
    public void TriggerHalfHealth() {
        // Save the player and boss hp into PlayerPrefs.
        PlayerPrefs.SetFloat("PlayerHP", battleSystem.Nagen.currentHP);
        PlayerPrefs.SetFloat("BossHP", battleSystem.Boss.currentHP);

        // Replace "50" with the appropriate values of allignment threshold to determine which scene to go to
        var scene = alignmentPoints >= 50 ? heroScene : villainScene;
        SceneManager.LoadScene(scene);
    }
}
