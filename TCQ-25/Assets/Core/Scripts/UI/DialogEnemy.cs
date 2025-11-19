using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogEnemy : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Text _speakText;
    [SerializeField] private GameObject _EButton,BalaoFala;

    [Header("Configs da fala")]
    [SerializeField] private float _textSpeed = 0.2f;
    [SerializeField] private string[] phrases;

    [Header("LiberarSkill")]
    [SerializeField] private int _skillToUnlock = 0;

    [HideInInspector] public int _speakIndex;

    private PlayerController _playerController;


    private void Start()
    {
        _speakIndex = 0;
    }

    public void getPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private IEnumerator TypePhrase() // Faz escrever  letrinha a letrinha
    {
        if (_speakIndex < phrases.Length - 1) _EButton.SetActive(true);

        _speakText.text = "";
        foreach (char letter in phrases[_speakIndex].ToCharArray())
        {
            _speakText.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    public void NextPhrase()
    {
        if (_speakText.text != phrases[_speakIndex])
        {
            StopAllCoroutines();
            _speakText.text = phrases[_speakIndex];
            return;
        }
        else if (_speakIndex < phrases.Length - 1)
        {
            _speakIndex++;
            StartCoroutine(TypePhrase());
        }
        else
        {
            UnlockSkill();
            _EButton.SetActive(false);
            BalaoFala.SetActive(false);
            _playerController._isBeingHit = false;
            _playerController.Speed = _playerController._baseSpeed;
        }

    }

    public void CallText() // Usado no evento de uma animação
    {
        StartCoroutine(TypePhrase());
    }

    private void UnlockSkill()
    {
        if (_skillToUnlock == 0) return;
        else if (_skillToUnlock > 0)
        {
            UIManager.UImanagerInstance.UnlockSkill(_skillToUnlock);

        }
    }
}
