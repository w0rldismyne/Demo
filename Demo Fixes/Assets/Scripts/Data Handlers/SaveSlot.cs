using UnityEngine;
using UnityEngine.UI;
using Lucerna.Utils;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private SaveLoadMenu saveLoadMenu;
    public int id;

    [Space(10)]
    [SerializeField] private Text dateTimeText = null;

    private void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => saveLoadMenu.SetID(this));

        try { 
            var timeData = JsonSerializer.ReadData<TimeData>("/DateTimes/save_" + id.ToString()); 
            SetText(timeData); 
        }
        catch { Debug.Log("Not Found"); dateTimeText.text = ""; }
    }

    public void SetText(TimeData data) {
        if (data == null) {
            dateTimeText.text = "";
            return;
        }
        dateTimeText.text = "Time: " + data.timePlayed.ToString("F0") + "\n" + "Date: " + data.date;
    }
}
