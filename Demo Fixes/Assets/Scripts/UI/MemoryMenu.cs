using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class MemoryMenu : MonoBehaviour
{
    [SerializeField] private Memory memoryPrefab = null;
    [SerializeField] private Transform content = null;
    [SerializeField] private MemorySO[] memoryObjects = null;

    [SerializeField] private GameObject fullscreenObject = null;
    [SerializeField] private Image fullscreenImage = null;

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

        for (int i = 0; i < memoryObjects.Length; i++) {
            if (!flowchart.GetBooleanVariable(memoryObjects[i].variableName)) {
                var memory = Instantiate(memoryPrefab, content);
                memory.Setup(fullscreenObject, fullscreenImage, memoryObjects[i]);
            }
        }
    }

    /*
    private void Update() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < memoryObjects.Length; i++) {
            if (!flowchart.GetBooleanVariable(memoryObjects[i].variableName)) {
                var memory = Instantiate(memoryPrefab, content);
                memory.Setup(fullscreenImage, memoryObjects[i]);
            }
        }
    }
    */
}
