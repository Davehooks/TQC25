using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Image _profile;
    public TMP_Text _speakText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _EButton;

    [Header("Configs da fala")]
    [SerializeField] private float _textSpeed = 0.2f;
    [SerializeField] private string[] phrases;

    [Header("LiberarSkill")]
    [SerializeField] private int _skillToUnlock = 0;


    private Animator _profileAnimator;
    [HideInInspector]public int _speakIndex;
    [SerializeField] private PlayerController player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
        _profileAnimator = _profile.GetComponent<Animator>();
        _speakIndex = 0;
    }

    private IEnumerator TypePhrase() // Faz escrever  letrinha a letrinha
    {
        if (_speakIndex < phrases.Length - 1) _EButton.SetActive(true);

        _speakText.text = "";
        _profileAnimator.SetBool("isTalking", true);
        foreach (char letter in phrases[_speakIndex].ToCharArray())
        {
            _audioSource.pitch = UnityEngine.Random.Range(0.0f, 1.0f); // é o que faz o baurulho de robô
            _speakText.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
        _profileAnimator.SetBool("isTalking", false);
    }

    public void NextPhrase()
    { 
        if (_speakText.text != phrases[_speakIndex])
        {
            StopAllCoroutines();
            _speakText.text = phrases[_speakIndex];
            _profileAnimator.SetBool("isTalking", false);
            return;
        }
        else if (_speakIndex < phrases.Length -1)
        {
            
            _speakIndex++;
            StartCoroutine(TypePhrase());
        }
        else
        {
            player.Speed = player._baseSpeed;
            UnlockSkill();
            _EButton.SetActive(false);
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
