using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Sprite[] _spriteButtons;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TMP_Text TMP_text;
    [SerializeField] private string text;
    [SerializeField] private float _textSpeed;
    private int _indexButton = 0;


    private void Start()
    {
        StartCoroutine(Text());
    }

    private void Update()
    {
        foreach (Button button in _buttons)
        {
            button.image.sprite = _spriteButtons[0];
        }
        _buttons[_indexButton].image.sprite = _spriteButtons[1];
    }

    IEnumerator Text()
    {
        foreach (char c in text)
        {
            TMP_text.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    public void IndexMinus(InputAction.CallbackContext context)
    {
        if (GameOverPanel.activeInHierarchy && context.performed)
        {
            Debug.Log("IndexMinus");
            _indexButton = (_indexButton - 1 + _buttons.Length) % _buttons.Length;
        }

    }
    public void IndexSelect(InputAction.CallbackContext context)
    {
        if (GameOverPanel.activeInHierarchy && context.performed)
        {
            switch (_indexButton)
            {
                case 0:
                    PlayAgain();
                    break;
                case 1:
                    BackMenu();
                    break;
                default:
                    Debug.Log($"Tá no index{_indexButton}");
                        break;
            }
        }
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
