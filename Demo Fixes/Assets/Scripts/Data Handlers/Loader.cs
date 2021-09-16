using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private SaveLoadMenu saveLoadMenu;
    private void Start() {
        Debug.Log(Time.timeScale);
        var val = PlayerPrefs.GetInt("LoadSlot", -1);
        if (val == -1) return;
        saveLoadMenu.Load(val);
        PlayerPrefs.SetInt("LoadSlot", -1);
    } 
}
