using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public AudioManager audioManager;

    public void MusicVolume()
    {
        audioManager.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        audioManager.SFXVolume(_sfxSlider.value);
    }
}
