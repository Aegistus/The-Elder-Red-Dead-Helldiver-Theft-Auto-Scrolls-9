using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }
	public int numOfPositionalSources = 100;

	public AudioMixerGroup mixerGroup;
	[SerializeField] string soundPath;

	//[SerializeField]
	private Sound[] sounds;

	private Queue<PositionalAudioSource> positionalSources = new Queue<PositionalAudioSource>();
	private List<AudioSource> toUnpause = new List<AudioSource>();

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			//DontDestroyOnLoad(gameObject);
		}
		sounds = Resources.LoadAll<Sound>(soundPath);
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.GetRandomAudioClip();
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
		Transform positionalSourceParent = new GameObject("Positional Audio Sources").transform;
        for (int i = 0; i < numOfPositionalSources; i++)
        {
			PositionalAudioSource newPositional = new GameObject("Positional Audio Source").AddComponent<PositionalAudioSource>();
			newPositional.transform.parent = positionalSourceParent;
			positionalSources.Enqueue(newPositional);
        }
	}

	/// <summary>
	/// Play sound with ID globally.
	/// </summary>
	/// <param soundID="soundID"> The ID for the sound. Can be retrieved with GetSoundID().</param>
	/// <returns>The AudioSource that is playing the sound. null if sound not found.</returns>
	public AudioSource PlaySoundGlobal(int soundID)
    {
		if (soundID < 0 || soundID >= sounds.Length)
		{
			Debug.LogWarning("Invalid Sound ID: " + soundID);
			return null;
		}
		Sound sound = sounds[soundID];

		sound.source.clip = sound.GetRandomAudioClip();
		sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

		sound.source.Play();
		return sound.source;
	}

	public AudioSource PlaySoundGlobal(string soundName)
	{
		return PlaySoundGlobal(Instance.GetSoundID(soundName));
	}

	public PositionalAudioSource PlaySoundAtPosition(string soundName, Vector3 position)
    {
		return PlaySoundAtPosition(Instance.GetSoundID(soundName), position);
    }

	/// <summary>
	/// Play sound with ID at the given position. The AudioSource will use 3D sound settings.
	/// </summary>
	/// <param soundID="soundID">The ID for the sound. Can be retrieved with GetSoundID().</param>
	/// <returns>The AudioSource that is playing the sound. null if sound not found.</returns>
	public PositionalAudioSource PlaySoundAtPosition(int soundID, Vector3 position)
    {
		if (soundID < 0 || soundID >= sounds.Length)
		{
			Debug.LogWarning("Invalid Sound ID: " + soundID);
			return null;
		}
		Sound sound = sounds[soundID];

		PositionalAudioSource source = positionalSources.Dequeue();
		source.SetSound(sound);
		source.transform.position = position;
		source.Play();
		positionalSources.Enqueue(source);
		return source;
	}

	/// <summary>
	/// Play sound with ID at position. Takes an extra parameter for parenting the AudioSource.
	/// </summary>
	/// <param soundID="soundID">The ID for the sound. Can be retrieved with GetSoundID().</param>
	/// <param parent="parent"> The transform the AudioSource should be parented to. </param>
	/// <returns>The AudioSource that is playing the sound. null if not found.</returns>
	public PositionalAudioSource PlaySoundAtPosition(int soundID, Vector3 position, Transform parent)
	{
		PositionalAudioSource source = PlaySoundAtPosition(soundID, position);
		source.SetFollowTarget(parent);
		return source;
	}

	public PositionalAudioSource PlaySoundAtPositionRandomDelay(int soundID, Vector3 position, float delayMax)
	{
		if (soundID < 0 || soundID >= sounds.Length)
		{
			Debug.LogWarning("Invalid Sound ID: " + soundID);
			return null;
		}
		Sound sound = sounds[soundID];

		PositionalAudioSource source = positionalSources.Dequeue();
		float delay = UnityEngine.Random.Range(0, delayMax);
		source.SetSound(sound);
		source.SetDelay(delay);
		source.transform.position = position;
		source.Play();
		positionalSources.Enqueue(source);
		return source;
	}

	/// <summary>
	/// Stops playing the sound with ID soundID.
	/// </summary>
	/// <param soundID="soundID">The ID for the sound. Can be retrieved with GetSoundID().</param>
	/// <returns>The AudioSource that is playing the sound. null if not found.</returns>
	public void StopPlayingGlobal(int soundID)
    {
		Sound sound = sounds[soundID];
		if (sound.source != null)
		{
			sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
			sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

			sound.source.Stop();
		}
	}

	public void StopPlayingGlobal(string soundName)
	{
		StopPlayingGlobal(GetSoundID(soundName));
	}

	/// <summary>
	/// Gets the integer identifier of a sound with string name. The ID will be greater than 0 and less than
	/// the total number of sounds.
	/// Logs a warning if the sound is not found.
	/// </summary>
	/// <param name="name">The name of the sound, defined in the Sound Scriptable Object.</param>
	/// <returns>The soundID. -1 if the sound is not found.</returns>
	public int GetSoundID(string name)
    {
		int id = Array.FindIndex(sounds, sound => sound.name == name);
		if (id == -1)
        {
			Debug.LogWarning("Sound with name: " + name + " does not exist!");
        }
		return id;
    }

	public void PlayGlobalFadeIn(int soundID, float fadeTime)
	{
		StartCoroutine(FadeIn(soundID, fadeTime));
	}

	public void StopPlayGlobalFadeOut(int soundID, float fadeTime)
	{
		StartCoroutine(FadeOut(soundID, fadeTime));
	}

	IEnumerator FadeIn(int soundID, float fadeTime)
    {
		if (soundID < 0 || soundID >= sounds.Length)
		{
			Debug.LogWarning("Invalid Sound ID: " + soundID);
		}
		else
        {
			Sound sound = sounds[soundID];

			sound.source.clip = sound.GetRandomAudioClip();
			sound.source.volume = 0;
			sound.source.Play();

			float currentTime = 0f;
			while (currentTime < fadeTime)
			{
				currentTime += Time.deltaTime;
				sound.source.volume = Mathf.Lerp(0, sound.volume, currentTime / fadeTime);
				yield return null;
			}
		}
    }

	IEnumerator FadeOut(int soundID, float fadeTime)
	{
		if (soundID < 0 || soundID >= sounds.Length)
		{
			Debug.LogWarning("Invalid Sound ID: " + soundID);
		}
		else
        {
			Sound sound = sounds[soundID];

			sound.source.clip = sound.GetRandomAudioClip();

			float currentTime = 0;
			while (currentTime < fadeTime)
			{
				currentTime += Time.deltaTime;
				sound.source.volume = Mathf.Lerp(0, sound.volume, 1 - (currentTime / fadeTime));
				yield return null;
			}
			sound.source.Stop();
		}
	}

	public void PauseAllSounds()
    {
		AudioSource[] sources = FindObjectsOfType<AudioSource>();
		for (int i = 0; i < sources.Length; i++)
        {
			if (sources[i].isPlaying)
            {
				sources[i].Pause();
				toUnpause.Add(sources[i]);
            }
        }
    }

	public void UnPauseAllSounds()
    {
		for (int i = 0; i < toUnpause.Count; i++)
        {
			toUnpause[i].UnPause();
        }
		toUnpause.Clear();
    }
}
