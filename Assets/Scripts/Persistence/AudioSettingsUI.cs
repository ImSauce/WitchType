using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private void Start()
    {
        // Load saved prefs into sliders
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.4f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Save changes back to PlayerPrefs
        musicSlider.onValueChanged.AddListener(v =>
        {
            PlayerPrefs.SetFloat("MusicVolume", v);
            PlayerPrefs.Save();
        });

        sfxSlider.onValueChanged.AddListener(v =>
        {
            PlayerPrefs.SetFloat("SFXVolume", v);
            PlayerPrefs.Save();
        });
    }
}
