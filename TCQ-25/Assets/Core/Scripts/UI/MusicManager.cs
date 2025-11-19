using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public enum MusicState {Menu, Exploration, Battle}

    [Header("Referências")]
    public MusicState currentMusicState = MusicState.Menu;
    public MusicState previousMusicState;

    public static MusicManager MusicInstance; // fazer com que possa ser chamado independente do lugar
    [Header("Configuracoes")]
    public AudioClip[] tracks;

    private AudioSource musicSource;

    private void Awake()
    {
        if (MusicInstance == null)
        {
            MusicInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        LoadMusic();
        PlayTrack(currentMusicState);
        previousMusicState = currentMusicState;
    }

    private void LoadMusic()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "SampleScene":
                currentMusicState = MusicState.Exploration;
                break;
            case "MenuScene":
                currentMusicState = MusicState.Menu;
                break;
            case "Boss":
                currentMusicState = MusicState.Battle;
                break;
        }
        if (currentMusicState != previousMusicState)
        {
            previousMusicState = currentMusicState;
            PlayTrack(currentMusicState);
        }    
    }
    public void PlayTrack(MusicState state)
    {
        int trackIndex=0;
        if(state == MusicState.Menu)
        {
            trackIndex = 0;
        }
        else if (state == MusicState.Exploration)
        { 
            trackIndex = 1;
        }
        else if(state == MusicState.Battle)
        {
            trackIndex = 2;
        }
        musicSource.clip = tracks[trackIndex];
        musicSource.Play();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadMusic();
    }

}
