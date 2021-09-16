using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class StatsDisplay : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private bool runOnUpdate = false;

    [Space(10)]
    [SerializeField] private Image[] charismaBoxes = null;
    [SerializeField] private Text charismaText = null;

    [Space(10)]
    [SerializeField] private Image[] intBoxes = null;
    [SerializeField] private Text intText = null;

    [Space(10)]
    [SerializeField] private Image[] vigorBoxes = null;
    [SerializeField] private Text vigorText = null;

    [Space(10)]
    [SerializeField] private Image[] visionBoxes = null;
    [SerializeField] private Text visionText = null;

    private Flowchart flowchart;

    // EXECUTION FUNCTIONS
    private void Awake() {
        if (flowchart == null) flowchart = GameObject.FindGameObjectWithTag("MainFlowchart").GetComponent<Flowchart>();
    }

    private void OnEnable() {
        if (flowchart == null) {
            Debug.LogError("InventoryMenu::OnEnable() --- No flowchart found! Did you forget to tag it with \"MainFlowchart\"?");
            return;
        }

        Calculate("Charm", charismaText, charismaBoxes, 20);
        Calculate("Intel", intText, intBoxes, 20);
        Calculate("Vigor", vigorText, vigorBoxes, 20);
        Calculate("Vision", visionText, visionBoxes, 20);
    }

    private void Update() {
        if (!runOnUpdate) return;
        Calculate("Charm", charismaText, charismaBoxes, 20);
        Calculate("Intel", intText, intBoxes, 20);
        Calculate("Vigor", vigorText, vigorBoxes, 20);
        Calculate("Vision", visionText, visionBoxes, 20);
    }

    // METHODS
    
    // Helper function to calculate which squares should be filled and by how much
    private void Calculate(string varName, Text text, Image[] boxArray, float max) {
        // Helper variables
        float varVal = flowchart.GetFloatVariable(varName);
        var quarter = max / 4;

        float textVal = 0;

        // Go through all the boxes
        for (int i = 0; i < boxArray.Length; i++) {
            var floor = ((max - quarter * i) - quarter); // The floor of the square (0, 5, 10, 15)
            var roof = (max - quarter * i); // The roof of the square (5, 10, 15, 20)

            // If the val is lower than this square
            if (varVal <= floor) { boxArray[i].fillAmount = 0; continue; }

            // If the val is higher than this square
            if (varVal > roof) { boxArray[i].fillAmount = 1; continue; }

            // The only option left is if the value is within this square
            textVal = (varVal - floor) / quarter;
            boxArray[i].fillAmount = textVal;
        }

        textVal = textVal == 1 ? 0 : textVal;
        text.text = (textVal * 100).ToString("F0") + "%";
    }
}
