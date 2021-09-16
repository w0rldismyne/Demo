using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fungus;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, ANIMATION, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    // VARIABLES
    [Header("Units")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Unit Nagen { get; private set; }
    public Unit Boss { get; private set; }

    public HitAreas hitAreas { get; private set; }

    [Space(10)]
    public BattleState state;

    [Header("UI")]
    public TextMeshProUGUI dialogueText;

    private Coroutine aiAttack;
    [SerializeField] private Flowchart healthHintFlowchart;
    private Flowchart activeHealthHintFlowchart = null;

    [SerializeField] private Text damageText = null;
    [SerializeField] private Sprite attackSprite = null;
    [SerializeField] private string finaleScene = "";

    private bool isBlocking = false;

    // EXECUTION FUNCTIONS
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    private void Update() {
        BattleStateMachine();
    }

    // METHODS
    void BattleStateMachine() {
        switch (state)
        {
            // === START STATE ===
            case BattleState.START:
                // Check if the Boss is dead
                if (Boss.currentHP <= 0) {
                    ChangeState(BattleState.WON);
                    break;
                }
                
                // Check if the player is dead
                else if (Nagen.currentHP <= 0) {
                    ChangeState(BattleState.LOST);
                    break;
                }

                // Go to the player turn
                dialogueText.text = "New Turn!";
                ChangeState(BattleState.PLAYERTURN);
                
                break;

            // === PLAYER TURN STATE ===
            case BattleState.PLAYERTURN:
         
                // The player turn is handled by the buttons on the menu
                break;

            // === ENEMY TURN STATE ===
            case BattleState.ENEMYTURN:
                // Check if the boss is dead from the player turn    
                if (Boss.currentHP <= 0) {
                    ChangeState(BattleState.WON);
                    break;
                }

                // Prevent the attack from being called multiple times
                if (aiAttack != null) break;

                // Attack
                AIAttack();
                dialogueText.text = "Enemy is attacking!";

                break;

              
           

            // === ANIMATION STATE ===
            case BattleState.ANIMATION:
                // Used for placeholder state while animations play (Animations are triggered in their
                // designated functions i.e. TriggerAttackMode, AIAttack, TriggerBlock, etç)
                break;

            // === WON STATE ===
            case BattleState.WON:
                Invoke("GoToFinale", 1f);
                dialogueText.text = "Nagen Won!";
                break;
            
            // === LOST STATE ===
            case BattleState.LOST:
                Invoke("GoToFinale", 1f);
                dialogueText.text = "Nagen Lost!";
                break;
        }
    }

    // === BLOCK ===
    public void TriggerBlock() {
        isBlocking = true;
        ChangeState(BattleState.ANIMATION);

        StartCoroutine(BlockCoroutine());
    }

    private IEnumerator BlockCoroutine() {
        dialogueText.text = "Nagen is blocking!";

        // Play Animation
        yield return new WaitForSeconds(1);

        ChangeState(BattleState.ENEMYTURN);
    }

    // === PLAYER ATTACK ===

    public void TriggerAttackMode() {
        hitAreas.Show();
    }

    public void PlayerAttack(float damageMultiplier) {
        ChangeState(BattleState.ANIMATION);

        hitAreas.Hide();

        StartCoroutine(AttackCoroutine(damageMultiplier));
    }

    private IEnumerator AttackCoroutine(float damageMultiplier) {
        dialogueText.text = "Nagen is attacking!";

        // Play Animation
        yield return new WaitForSeconds(1);

        Boss.currentHP -= (Nagen.damage * damageMultiplier);
        ChangeState(BattleState.ENEMYTURN);
    }

    // === AI ATTACK ===
    private void AIAttack() {
        ChangeState(BattleState.ANIMATION);

        aiAttack = StartCoroutine(AIAttackCoroutine());
    }

    private IEnumerator AIAttackCoroutine() {
        dialogueText.text = "Midnight Refrain is attacking!";
        // Switching sprites acts as animation like you requested in the order.
        // If you wanna add more, you could either create an actual animation
        // or add more sprites like I did here!
        var idleSprite = Boss.GetComponentInChildren<Image>().sprite;
        Boss.GetComponentInChildren<Image>().sprite = attackSprite; // Swap sprite to attack sprite
        yield return new WaitForSeconds(1f);                        // Wait 1 second
        Boss.GetComponentInChildren<Image>().sprite = idleSprite;   // Swap sprite to idle sprite again

        // Reset the attack coroutine
        aiAttack = null;

        // Determine damage with blocking or not blocking
        Nagen.currentHP -= isBlocking ? 0 : Boss.damage;

        // Show damage text
        damageText.text = isBlocking ? "Blocked" : "-" + Boss.damage.ToString();
        damageText.gameObject.SetActive(true);

        // Reset blocking
        isBlocking = false;

        yield return new WaitForSeconds(1f);

        damageText.gameObject.SetActive(false);

        // Destroy the current health hint flowchart
        if (activeHealthHintFlowchart != null)
            Destroy(activeHealthHintFlowchart.gameObject);
        // Update the health hint flowchart prefab
        healthHintFlowchart.SetFloatVariable("PlayerHealth", Nagen.currentHP);
        // Create a new health hint flowchart
        activeHealthHintFlowchart = Instantiate(healthHintFlowchart);

        ChangeState(BattleState.START);
    }

    /// <summary>
    /// Change state to newState, you can set the timer to a value in seconds to
    /// delay the transition, for any reason, if not, just leave it empty and it'll transition instantly.
    /// </summary>
    /// <param name="newState"></param>
    /// <param name="timer"></param>
    public void ChangeState(BattleState newState, float timer=0f) {
        StartCoroutine(ChangeStateCoroutine(newState, timer));
    }

    private IEnumerator ChangeStateCoroutine(BattleState newState, float timer) {
        yield return new WaitForSeconds(timer);
        state = newState;
    }

    void SetupBattle()
    {
        Nagen = playerPrefab.GetComponent<Unit>();
        Boss = enemyPrefab.GetComponent<Unit>();

        hitAreas = enemyPrefab.GetComponent<HitAreas>();

        dialogueText.text = "Oh no" + Boss.unitName + "has declared war.";
    }

    private void GoToFinale() {
        SceneManager.LoadScene(finaleScene);
    }
    ///Dialoguestuff///
    public void Status()
    {
        if (Nagen.currentHP <= 30) dialogueText.text = "I've still got this!";
        if (Nagen.currentHP <= 20) dialogueText.text = "I've been better.";
        if (Nagen.currentHP <= 10) dialogueText.text = "I gotta get out of here fast.";
        
    }
}
