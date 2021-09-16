using UnityEngine.Audio;
using System;
using UnityEngine;
using Lucerna.Utils;

public class AudioManager : MonoBehaviour
{
	public AudioMixer audioMixer;
	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	private void Start() {
		AudioData data = null;

        try { data = JsonSerializer.ReadData<AudioData>("/Settings/Audio"); }
        catch { data = new AudioData(); }

		audioMixer.SetFloat("generalVolume", data.generalVolume);
        audioMixer.SetFloat("musicVolume", data.musicVolume);
        audioMixer.SetFloat("sfxVolume", data.sfxVolume);
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		//StopAllInMixer(s.mixerGroup.name);

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Play();
	}

	public void StopAll() {
		foreach (Sound s in sounds) {
			s.source.Stop();
		}
	}

	public void StopAllInMixer(string mixerName) {
		Sound[] ms = Array.FindAll(sounds, item => item.mixerGroup.name == mixerName);

		foreach (Sound s in ms) {
			s.source.Stop();
		}
	}
}
