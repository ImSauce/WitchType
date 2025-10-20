using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        // Detect scene and play music
        string currentScene = SceneManager.GetActiveScene().name;
        PlaySceneMusic(currentScene);
    }

    private void PlaySceneMusic(string sceneName)
    {
        switch (sceneName)
        {
            case "MAIN MENU":
                PlayMusic("Main Menu");
                break;

            case "CH1_L1_R1":
                PlayMusic("Soundtrack1");
                break;

            default:
                Debug.Log($"No specific music for scene: {sceneName}");
                break;
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning($"Music '{name}' not found!");
            return;
        }

        if (musicSource.clip == s.clip) return; // don't restart same song

        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning($"SFX '{name}' not found!");
            return;
        }

        sfxSource.PlayOneShot(s.clip);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
