using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundtrackAudioSource;
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    private int audioSourceIndex = 0;

    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this;
        }
    }


    private void Start()
    {
        if(soundtrackAudioSource == null || audioSources.Count == 0) 
        {
            Debug.LogError("Audio Manager is missing audio sources. Add them in the inspector");
        }    
    }
    
    /// <summary>
    /// Call this to play a SFX from any Class. This is to avoid having to set up Audio Sources
    /// on every clickable object. Those might get deleted and these audio sources will persist. 
    /// </summary>
    public void PlaySFX(AudioClip audioClip, float volume = 1.0f, bool isLooping = false)
    {
        AudioSource source = audioSources[audioSourceIndex];
        source.resource = audioClip;
        source.volume = volume;
        source.loop = isLooping;
        source.Play();
    
        // Iterate index so that next time we use the next audio source
        audioSourceIndex++;
        if(audioSourceIndex >= 5) 
        {
            audioSourceIndex = 0;
        }
    }
    
    /// <summary>
    /// We first pick a random audio clip from the list, and follow up by playing it by called `PlaySFX()` 
    /// </summary>
    public void PlaySFXFromList(List<AudioClip> audoClips, float volume = 1.0f, bool isLooping = false)
    {
        int clipIndex = Random.Range(0, audoClips.Count);
        PlaySFX(audoClips[clipIndex], volume, isLooping);
    }
    
    public void PlaySoundtrack(AudioClip audioClip, float volume = 1.0f, bool isLooping = true)
    {
        soundtrackAudioSource.clip = audioClip;
        soundtrackAudioSource.volume = volume; 
        soundtrackAudioSource.loop = isLooping;
    }
}
