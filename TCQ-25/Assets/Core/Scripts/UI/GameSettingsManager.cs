using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameSettingsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameSettingsManager SettingsInstance;

    [Header("Configs")]
    [SerializeField] private AudioMixer fxMixer;
    [SerializeField] private AudioMixer music;
    [SerializeField] private AudioMixer rainMixer;
    [SerializeField] private float musicVolume;
    [SerializeField] private float fxVolume;
    [SerializeField] private float rainVolume;
    private bool _isFullScreen;

    private void Awake()
    {
        LoadSettings();
        if (SettingsInstance == null)
        {
            SettingsInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
    }
    private void LoadSettings()
    {
        _isFullScreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.1f);
        fxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.1f);

        SetFullscreen(_isFullScreen);
        SetMusicVolume(musicVolume);
        SetFxVolume(fxVolume);
    }
    public void SetFullscreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("Fullscreen",isFullScreen ? 1 : 0);
    }

    public void SetMusicVolume(float volume)
    {

        musicVolume = volume;
        if (volume == 0)
            music.SetFloat("MusicVolume", -80);
        else
            music.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetFxVolume(float volume)
    {

        fxVolume = volume;
        if (volume == 0)
        {
            fxMixer.SetFloat("SFXVolume", -80);

        }
        else
        {
            fxMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("SFXVolume",volume);
        PlayerPrefs.Save();
    }

    public void SetRainVolume(float volume)
    {

        rainVolume = volume;
        if (volume == 0)
        {
            rainMixer.SetFloat("RainVolume", -80);

        }
        else
        {
            rainMixer.SetFloat("RainVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("RainVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }

    public float GetRainVolume()
    {
        return PlayerPrefs.GetFloat("RainVolume", 0.5f);
    }

    public bool IsFullScreen()
    {
        return _isFullScreen;
    }

}
