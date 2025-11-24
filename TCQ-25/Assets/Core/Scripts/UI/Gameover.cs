using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Sprite[] spriteButtons;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TMP_Text TMP_text;
    [SerializeField] private string text;
    [SerializeField] private float _textSpeed;
    private int _indexButton;


    private void Start()
    {
        StartCoroutine(Text());
    }

    public void BackMenu()
    {

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
        if (!GameOverPanel.activeInHierarchy && context.performed)
        {
            Debug.Log("IndexMinus");
            _indexButton = (_indexButton - 1 + _buttons.Length) % _buttons.Length;

        }

    }
    public void IndexSelect(InputAction.CallbackContext context)
    {
        if (!GameOverPanel.activeInHierarchy && context.performed)
        {
            switch (_indexButton)
            {
                case 0:
                    SceneManager.LoadScene("SampleScene");
                    break;
                case 1:
                    SceneManager.LoadScene("MenuScene");
                    break;
            }
        }
    }

}
