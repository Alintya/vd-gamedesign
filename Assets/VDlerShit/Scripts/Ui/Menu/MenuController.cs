using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;


public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Text masterVolumeTextValue = null;
    [SerializeField] private Slider masterVolumeSlider = null;

    [SerializeField] private TMP_Text musicVolumeTextValue = null;
    [SerializeField] private Slider musicVolumeSlider = null; 

    [SerializeField] private TMP_Text effectsVolumeTextValue = null;
    [SerializeField] private Slider effectsVolumeSlider = null;

    [SerializeField] private float defaultVolume = 50f;

    [Header("Graphics Settings")]

    [SerializeField] private Toggle fullScreenToggle;

    private bool _isFullScreen;


    [Header("Confirmation Box")]
    [SerializeField] private GameObject comfirmationPrompt = null;


    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown. RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float volume)
    {
        if (volume != 0)
        {
            audioMixer.SetFloat("master", Mathf.Log10(volume/100) * 20 + 6);
        }
        else
        {
            audioMixer.SetFloat("master", -80);
        }
        masterVolumeTextValue.text = volume.ToString("0");
    }
    public void SetMusicVolume(float volume)
    {
        if (volume != 0)
        {
            audioMixer.SetFloat("music", Mathf.Log10(volume/100) * 20 + 6);
        }
        else
        {
            audioMixer.SetFloat("music", -80);
        }
        musicVolumeTextValue.text = volume.ToString("0");
    }
    public void SetEffectsVolume(float volume)
    {
        if (volume != 0)
        {
            audioMixer.SetFloat("effects", Mathf.Log10(volume/100) * 20 + 6);
        }
        else
        {
            audioMixer.SetFloat("effects", -80);
        }
        effectsVolumeTextValue.text = volume.ToString("0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat (PlayerSettingsHelper.MasterVolume, masterVolumeSlider.value);
        PlayerPrefs.SetFloat (PlayerSettingsHelper.MusicVolume, musicVolumeSlider.value);
        PlayerPrefs.SetFloat (PlayerSettingsHelper.EffectsVolume, effectsVolumeSlider.value);
        StartCoroutine(ConfirmationBox());
    }

    public void SetFullScreen(bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetInt(PlayerSettingsHelper.MasterFullscreen, (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
        
        StartCoroutine(ConfirmationBox());
    }

    public void ResettButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if (MenuType == "Audio")
        {
            SetMasterVolume(defaultVolume);
            masterVolumeSlider.value = defaultVolume;
            SetMusicVolume(defaultVolume);
            musicVolumeSlider.value = defaultVolume;
            SetEffectsVolume(defaultVolume);
            effectsVolumeSlider.value = defaultVolume;
            
            VolumeApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }
}
