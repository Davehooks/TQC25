using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _configPanel;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Sprite[] _spriteButtons;
    [SerializeField] private Animator _anim;
    private int _indexButton;
    [SerializeField] private TMP_Text[] _textMeshPro;
    [SerializeField] private string[] textos;
    [SerializeField] private float textSpeed;

    void Start()
    {
        if (_buttons == null)
        {
            Debug.Log("Não tem botão no MenuSript");
        }
        _anim = GetComponent<Animator>();
        

    }
    void Update()
    {
        if (_textMeshPro[_indexButton].text == "")
        {
            for(int i = 0;i < _textMeshPro.Length;i++) { _textMeshPro[i].text = ""; }
            StartCoroutine(Text());
        }
            foreach (Button button in _buttons)
            {
                button.image.sprite = _spriteButtons[0];
            }
        _buttons[_indexButton].image.sprite = _spriteButtons[1];
    }

    public void IndexPlus(InputAction.CallbackContext context)
    {
        if (!_configPanel.activeInHierarchy && context.performed)
        {
            Debug.Log("IndexPlus");
        _indexButton = (_indexButton + 1 + _buttons.Length) % _buttons.Length;
        }
    }
    public void IndexMinus(InputAction.CallbackContext context)
    {
        if (!_configPanel.activeInHierarchy && context.performed)
        {
            Debug.Log("IndexMinus");
            _indexButton = (_indexButton - 1 + _buttons.Length) % _buttons.Length;
        
        }

    }
    public void IndexSelect(InputAction.CallbackContext context)
    {
        if (!_configPanel.activeInHierarchy && context.performed)
        {
            switch (_indexButton)
            {
                case 0: _anim.SetTrigger("Start");  // TODO mudar o nome disso aqui se for mudar o nome da cena
                    break;
                case 1: OpenConfigPanel();
                    break;
                case 2: SceneManager.LoadScene("Creditos");
                    break;
                case 3: QuitGame();
                    break;
            }
        }
    }

    IEnumerator Text()
    {
        foreach (TMP_Text texto in _textMeshPro)
        {
            texto.text = "";
        }
        foreach (char letter in textos[_indexButton])
        {
            _textMeshPro[_indexButton].text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenConfigPanel()
    {
        _configPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClosePanel(InputAction.CallbackContext context)
    {

        if(_configPanel.activeInHierarchy)
        {
        UIManager.UImanagerInstance.ClosePanel();
        }
    }

}
