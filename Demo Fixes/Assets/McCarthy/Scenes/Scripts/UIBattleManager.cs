using UnityEngine;

/// <summary>
/// This class just improves the quality of life of the player. So the menu will turn off if it's NOT
/// the players turn. The blocker object is to prevent button clicking when it is NOT the players turn.
/// </summary>
public class UIBattleManager : MonoBehaviour
{
    private BattleSystem battleSystem;
    [SerializeField] private GameObject blockerObject = null;
    [SerializeField] private GameObject menuObject = null;

    private void Start() {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void Update() {
        blockerObject.SetActive(battleSystem.state != BattleState.PLAYERTURN);
        // Comment this line out if you want the menu to be there while the player attack.
        // The blocker object (line above) will prevent the player from pressing any buttons anyway.
        menuObject.SetActive(battleSystem.state == BattleState.PLAYERTURN);
    }
}
