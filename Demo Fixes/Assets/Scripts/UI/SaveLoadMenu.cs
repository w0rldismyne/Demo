using UnityEngine;
using Fungus;
using Lucerna.Utils;
using System;
using UnityEngine.SceneManagement;

public class SaveLoadMenu : MonoBehaviour
{
    [SerializeField] private GameObject prompt = null;

    private SaveSlot currentSlot;
    private SaveMenu saveMenu;

    private void Awake() => saveMenu = FindObjectOfType<SaveMenu>();

    public void SetID(SaveSlot saveSlot) {
        currentSlot = saveSlot;
        prompt.SetActive(true);
    }

    public void Save() {
        saveMenu.Save(currentSlot.id);
        prompt.SetActive(false);

        var timeData = new TimeData(TimeTracker.instance.time, DateTime.Now);
        JsonSerializer.SaveData<TimeData>(timeData, "DateTimes", "save_" + currentSlot.id.ToString());

        currentSlot.SetText(timeData);
    }

    public void Load(int external=-1) {
        var val = external == -1 ? currentSlot.id : external;

        try { 
            var timeData = JsonSerializer.ReadData<TimeData>("/DateTimes/save_" + val.ToString()); 
            TimeTracker.instance.time = timeData.timePlayed;
        }
        catch { return; }

        if (external != -1) {
            if (saveMenu == null) saveMenu = FindObjectOfType<SaveMenu>();
            saveMenu.Load(val);
        }
        else {
            PlayerPrefs.SetInt("LoadSlot", val);
            try { Lucerna.Utils.SceneLoader.instance.LoadSceneAsync(SceneManager.GetActiveScene().name); }
            catch { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
        }

        prompt.SetActive(false);
    }

    public void Delete() {
        saveMenu.Delete(currentSlot.id);
        JsonSerializer.Delete("/DateTimes/save_" + currentSlot.id.ToString());
        currentSlot.SetText(null);
    }

    public void MenuLoad() {
        PlayerPrefs.SetInt("LoadSlot", currentSlot.id);
        saveMenu.Load(currentSlot.id);
    }
}
