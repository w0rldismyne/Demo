using UnityEngine;

/// <summary>
/// Helper function for the hit zones that informs the battle system if an attack has been done
/// </summary>
public class SpriteAttackPoint : MonoBehaviour
{
    public float damageMultiplier;
    private BattleSystem battleSystem;
    private Unit myUnit;

    private void Awake() {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    public void Attack() {
        battleSystem.PlayerAttack(damageMultiplier);
    }
}
