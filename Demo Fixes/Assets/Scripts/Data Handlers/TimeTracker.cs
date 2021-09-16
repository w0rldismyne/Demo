using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public static TimeTracker instance;
    public float time;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() {
        time += Time.deltaTime;
    }
}
