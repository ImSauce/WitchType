using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("-------------[ Audio Source ]-------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("-------------[ BGM Clips ]-------------")]
    public AudioClip[] backgroundTracks;

    [Header("-------------[ SFX Clips ]-------------")]
    public AudioClip collect;
    public AudioClip win;
    public AudioClip death;
    public AudioClip UI_click;

    [Header("-------------[ Entity Clips ]-------------")]
    public AudioClip player_walk_grass;
    public AudioClip player_walk_stone;
    public AudioClip player_walk_wood;

    [Header("-------------[ Sliders ]-------------")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private int currentTrackIndex = 0;

    private void Start()
    {
        // Load saved volumes first
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.4f); // default 1
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);

        // Apply to sliders (if assigned)
        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        // Play music
        if (backgroundTracks.Length > 0)
        {
            PlayBackground(currentTrackIndex);
        }
    }

    public void PlayBackground(int index)
    {
        if (index < 0 || index >= backgroundTracks.Length)
        {
            Debug.LogWarning("Invalid track index!");
            return;
        }

        currentTrackIndex = index;
        musicSource.clip = backgroundTracks[index];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
        PlayerPrefs.Save();
    }

    public void Mute()
    {
        musicSource.mute = true;
    }

    public void Unmute()
    {
        musicSource.mute = false;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void CollideSFX(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip, 1f);
    }
}
