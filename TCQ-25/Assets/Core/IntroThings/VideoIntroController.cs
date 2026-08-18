using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // É necessário para carregar cenas

public class VideoIntroController : MonoBehaviour
{
    // Crie uma variável pública para atribuir o VideoPlayer no Inspector
    public VideoPlayer videoPlayer;

    // O nome da próxima cena que você deseja carregar
    public string nextSceneName = "NomeDaProximaCena";

    void Start()
    {
        // 1. Verifique se o VideoPlayer foi atribuído
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer não está atribuído no Inspector!");
            return;
        }

        // 2. Assine o método OnVideoFinished ao evento loopPointReached
        // Este método será chamado quando o vídeo terminar.
        videoPlayer.loopPointReached += OnVideoFinished;

        // **OPCIONAL:** Certifique-se de que o loop esteja desativado
        // se você quer que o vídeo toque apenas uma vez
        videoPlayer.isLooping = false;
    }

    // Método chamado quando o evento loopPointReached é disparado
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Vídeo de introdução finalizado! Carregando a próxima cena...");

        // 3. Chame o carregamento da próxima cena
        SceneManager.LoadScene(nextSceneName);
    }

    // Boa prática: Remova a inscrição do evento ao desativar/destruir o objeto
    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}