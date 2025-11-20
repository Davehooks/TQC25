using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UImanagerInstance;


    [Header("SettingsUI")]
    [SerializeField] public GameObject _SettingsPanel;
    [SerializeField] private Slider _MusicVolumeSliders;
    [SerializeField] private Slider _FXVolumeSliders;
    [SerializeField] private Toggle _isFullScreen;
    [SerializeField] private AudioSource _testSound;

    [Header("InGame")]
    private CallHud callhud;

    private PlayerController _playerController;
    
   

    private bool gameStarted = false;

    private void Awake()
    {
        if (UImanagerInstance == null)
        {
            UImanagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(_playerController == null && SceneManager.GetActiveScene().name == "SampleScene")
            FindAnyObjectByType(typeof(PlayerController));
        callhud =GetComponent<CallHud>();
        _isFullScreen.isOn = GameSettingsManager.SettingsInstance.IsFullScreen();
        _FXVolumeSliders.value = GameSettingsManager.SettingsInstance.GetFXVolume();
        _MusicVolumeSliders.value = GameSettingsManager.SettingsInstance.GetMusicVolume();
        OnFxVolumeChanged(_FXVolumeSliders.value);
        OnMusicVolumeChanged(_MusicVolumeSliders.value);

        _isFullScreen.onValueChanged.AddListener(OnFullscreenChanged);
        _FXVolumeSliders.onValueChanged.AddListener(OnFxVolumeChanged);
        _MusicVolumeSliders.onValueChanged.AddListener(OnMusicVolumeChanged);

    }
    public void OnFxVolumeChanged(float volume) // vai no Slider FX
    {
        GameSettingsManager.SettingsInstance.SetFxVolume(volume);

        if (!gameStarted ) { gameStarted = true;return; }

        if (!_testSound.isPlaying)
        {
            _testSound.Play();
        }
    }
    public void OnRainVolumeChanged(float volume)
    {
        GameSettingsManager.SettingsInstance.SetRainVolume(volume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        GameSettingsManager.SettingsInstance.SetMusicVolume(volume);
    }

    public void OnFullscreenChanged(bool isFullscreen)
    {
        GameSettingsManager.SettingsInstance.SetFullscreen(isFullscreen);
    }

    private void OnDestroy()
    {
        _isFullScreen.onValueChanged.RemoveListener(OnFullscreenChanged);
        _FXVolumeSliders.onValueChanged.RemoveListener(OnFxVolumeChanged);
        _MusicVolumeSliders.onValueChanged.RemoveListener(OnMusicVolumeChanged);
    }

    public void ClosePanel()
    {
        _SettingsPanel.SetActive(false);
    }

    public void UnlockSkill(int i)
    {
        callhud.EnableSkill(i);
    }

    public void ModoHud(int modo)
    {
        callhud.ChangeModo(modo);
    }

    public void MorseHud(char morse)
    {
        callhud.MorseCode(morse);
    }

    public void ZerarMorse()
    {
        callhud.ZerarMorse();
    }
    public void VidaHUD()
    {
        callhud.VidaPersonagemHUD();
    }

}
