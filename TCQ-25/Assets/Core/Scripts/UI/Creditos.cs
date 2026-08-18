using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    [SerializeField] private float _textSpeed;
    [SerializeField] private TMP_Text[] _texts;
    [TextArea(5, 8)]
    [SerializeField] private string[] _text;
    [SerializeField] int _textIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckText();
    }

IEnumerator Text(int index)
    {
        foreach(char c in _text[index])
        {
            _texts[index].text += c;
             yield return new WaitForSeconds(_textSpeed);
        }
        _textIndex++;
        CheckText();
    }
    private void CheckText()
    {
        if (_textIndex < _texts.Length)
        {
            StartCoroutine(Text(_textIndex));
        }
        else
        {
            return;
        }
    }
public void backMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
