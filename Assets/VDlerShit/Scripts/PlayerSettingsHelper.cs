using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettingsHelper : MonoBehaviour
{
    public const string MasterVolume = "masterVolume";
    public const string MusicVolume = "musicVolume";
    public const string EffectsVolume = "effectsVolume";
    public const string MasterFullscreen = "masterFullscreen";


    [Header("General Settings")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text masterVolumeTextValue = null;
    [SerializeField] private Slider masterVolumeSlider = null;

    [SerializeField] private TMP_Text musicVolumeTextValue = null;
    [SerializeField] private Slider musicVolumeSlider = null; 

    [SerializeField] private TMP_Text effectsVolumeTextValue = null;
    [SerializeField] private Slider effectsVolumeSlider = null;

    [Header("Fullscreen Settings")]
    [SerializeField] private Toggle fullScreenToggle;

    private void Awake()
    {
        if (canUse)
        {
            if(PlayerPrefs.HasKey(MasterVolume))
            {
                float localVolume = PlayerPrefs.GetFloat(MasterVolume);
                menuController.SetMasterVolume(localVolume);
                masterVolumeSlider.value = localVolume;
            }
            if(PlayerPrefs.HasKey(MusicVolume))
            {
                float localVolume = PlayerPrefs.GetFloat(MusicVolume);
                menuController.SetMusicVolume(localVolume);
                musicVolumeSlider.value = localVolume;
            }
            if(PlayerPrefs.HasKey(EffectsVolume))
            {
                float localVolume = PlayerPrefs.GetFloat(EffectsVolume);
                menuController.SetEffectsVolume(localVolume);
                effectsVolumeSlider.value = localVolume;
            }
            

            if (PlayerPrefs.HasKey(MasterFullscreen))
            {
                int localFullscreen = PlayerPrefs.GetInt(MasterFullscreen);

                if (localFullscreen ==1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }
        }
    }
}
