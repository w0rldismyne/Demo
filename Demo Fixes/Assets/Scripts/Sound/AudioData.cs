[System.Serializable]
public class AudioData
{
    public float generalVolume;
    public float musicVolume;
    public float sfxVolume;

    public AudioData() {
        generalVolume = 0;
        musicVolume = 0;
        sfxVolume = 0;
    }

    public AudioData(AudioData data) {
        generalVolume = data.generalVolume;
        musicVolume = data.musicVolume;
        sfxVolume = data.sfxVolume;
    }

    public AudioData(float g, float m, float s) {
        generalVolume = g;
        musicVolume = m;
        sfxVolume = s;
    }
} 
