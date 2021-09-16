using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private Evidence evidencePrefab = null;
    [SerializeField] private Transform content = null;
    [SerializeField] private EvidenceSO[] evidenceObjects = null;

    [SerializeField] private Text descriptionText = null;

    private Flowchart flowchart;

    private void OnEnable() {
        if (flowchart == null) flowchart = GameObject.FindGameObjectWithTag("MainFlowchart").GetComponent<Flowchart>();

        if (flowchart == null) {
            Debug.LogError("InventoryMenu::OnEnable() --- No flowchart found! Did you forget to tag it with \"MainFlowchart\"?");
            return;
        }

        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < evidenceObjects.Length; i++) {
            if (!flowchart.GetBooleanVariable(evidenceObjects[i].variableName)) {
                var evidence = Instantiate(evidencePrefab, content);
                evidence.Setup(descriptionText, evidenceObjects[i]);
            }
        }
    }

    private void Update() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < evidenceObjects.Length; i++) {
            if (!flowchart.GetBooleanVariable(evidenceObjects[i].variableName)) {
                var evidence = Instantiate(evidencePrefab, content);
                evidence.Setup(descriptionText, evidenceObjects[i]);
            }
        }
    }
}
