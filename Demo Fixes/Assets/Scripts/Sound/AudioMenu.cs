using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Lucerna.Utils;

public class AudioMenu : MonoBehaviour
{
    // VARIABLES

    private AudioMixer audioMixer;
    [Space(10)]
    [SerializeField] private Slider generalSlider = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    private AudioData currentAudioData;

    // EXECUTION METHODS

    private void Awake() {
        audioMixer = FindObjectOfType<AudioManager>().audioMixer;
        InitData();
    }

    // METHODS

    public void RestoreDefaults() {
        currentAudioData.generalVolume = 0f;
        currentAudioData.musicVolume = 0f;
        currentAudioData.sfxVolume = 0f;

        generalSlider.value = 0f;
        musicSlider.value = 0f;
        sfxSlider.value = 0f;

        audioMixer.SetFloat("generalVolume", 0f);
        audioMixer.SetFloat("musicVolume", 0f);
        audioMixer.SetFloat("sfxVolume", 0f);

        JsonSerializer.SaveData(currentAudioData, "Settings", "Audio");
    }

    public void SetGeneral(float val) {
        audioMixer.SetFloat("generalVolume", val);
        currentAudioData.generalVolume = val;

        JsonSerializer.SaveData(currentAudioData, "Settings", "Audio");
    }

    public void SetMusic(float val) {
        audioMixer.SetFloat("musicVolume", val);
        currentAudioData.musicVolume = val;

        JsonSerializer.SaveData(currentAudioData, "Settings", "Audio");
    }

    public void SetSfx(float val) {
        audioMixer.SetFloat("sfxVolume", val);
        currentAudioData.sfxVolume = val;

        JsonSerializer.SaveData(currentAudioData, "Settings", "Audio");
    }

    private void InitData() {       
        AudioData data = null;

        try { data = JsonSerializer.ReadData<AudioData>("/Settings/Audio"); }
        catch { data = new AudioData(); }
        
        currentAudioData = new AudioData(data);

        generalSlider.value = currentAudioData.generalVolume;
        musicSlider.value = currentAudioData.musicVolume;
        sfxSlider.value = currentAudioData.sfxVolume;
	}
}