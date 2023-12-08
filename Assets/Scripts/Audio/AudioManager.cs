using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicAudioSource;
    public AudioSource soundEffectAudioSource;

    private Dictionary <string, AudioClip> audioFiles;

    private void Awake()
    {
        audioFiles = InitAudioDictionary();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.audioEvents.onPlayOneShot += PlayOneShot;
        GameEventsManager.instance.audioEvents.onPlay += Play;
        GameEventsManager.instance.audioEvents.onStop += Stop; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.audioEvents.onPlayOneShot -= PlayOneShot;
        GameEventsManager.instance.audioEvents.onPlay -= Play;
        GameEventsManager.instance.audioEvents.onStop -= Stop;
    }

    private void Start()
    {
        //GameEventsManager.instance.audioEvents.Play("Mus_OpenTrip");
    }

    private Dictionary<string, AudioClip> InitAudioDictionary()
    {
        // loads all the audioclips under the Assets/Resources/Sounds folder
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Sounds");

        Dictionary<string, AudioClip> soundEffectsDictionary = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in audioClips)
        {
            soundEffectsDictionary.Add(clip.name, clip);
        }

        return soundEffectsDictionary;
    }

    public void PlayOneShot(string name)
    {
        if (audioFiles.TryGetValue(name, out AudioClip clip))
        {
            soundEffectAudioSource.PlayOneShot(audioFiles[name]);
        }
        else
        {
            Debug.Log("Audio clip not found with name :" + name);
        }
    }

    public void Play(string name)
    {
        if (audioFiles.TryGetValue(name, out AudioClip clip))
        {
            backgroundMusicAudioSource.clip = audioFiles[name];
            backgroundMusicAudioSource.loop = true;
            backgroundMusicAudioSource.Play();
        }
        else
        {
            Debug.Log("Audio clip not found with name :" + name);
        }
    }

    public void Stop()
    {
        backgroundMusicAudioSource?.Stop();
        soundEffectAudioSource?.Stop();
    }

}

