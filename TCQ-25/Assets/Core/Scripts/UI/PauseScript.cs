using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button[] buttons;
    private int _indexButton;
    [SerializeField] private Image PauseImage;
    [SerializeField] private Sprite[] pauseSprites;

    private void Start()
    {
        if (buttons == null)
        {
            Debug.Log("Tá faltando botão no PauseScript");
        }
    }

    private void Update()
    {
        if (_pausePanel != null &&_pausePanel.activeInHierarchy)
        {
            foreach (Button buttons in buttons)
            {
                buttons.image.color = Color.white;
            }
            buttons[_indexButton].image.color = Color.lightGreen;
        }
        else return;
    }
    public void CallPause(InputAction.CallbackContext input)
    {
        if(input.performed)
        {
        if (!_pausePanel.activeInHierarchy)
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0 && !UIManager.UImanagerInstance._SettingsPanel.activeInHierarchy)
        {
            _pausePanel.SetActive(false);
             Time.timeScale = 1;
        }
        else if(UIManager.UImanagerInstance._SettingsPanel.activeInHierarchy)
        {
            UIManager.UImanagerInstance.ClosePanel();

        }
        }

    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenOptions()
    {
        UIManager.UImanagerInstance._SettingsPanel.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MenuScene");
    }

}

