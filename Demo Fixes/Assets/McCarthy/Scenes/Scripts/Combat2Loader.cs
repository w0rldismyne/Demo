using UnityEngine;

/// <summary>
/// Loads up the player and boss hp when they come back for combat 2.
/// </summary>
public class Combat2Loader : MonoBehaviour
{
    [SerializeField] private Unit playerUnit;
    [SerializeField] private Unit enemyUnit;

    private void Start() {
        playerUnit.currentHP = PlayerPrefs.GetFloat("PlayerHP");
        enemyUnit.currentHP = PlayerPrefs.GetFloat("BossHP");
    }
}
