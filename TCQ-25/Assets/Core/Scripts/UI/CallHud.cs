using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CallHud : MonoBehaviour
{
    [SerializeField] private Sprite[] _vidas; // sprites de vida cheia e 
    [SerializeField] private RuntimeAnimatorController[] _playerModosController; // Animator da tela do robô
    [SerializeField] private Animator _modosAnimator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Sprite[] MorseSprites;

    [Header("Referencias da HUD")]
    [SerializeField] private GameObject[] _vida;
    [SerializeField] private Image[] _morseCodesToEnable;
    [SerializeField] private int _morseIndex;
    [SerializeField] private Image morse;
    [SerializeField] private Image[] morseElements;
    [SerializeField] private Image modo, Personagem;

    private void Start()
    {
        _playerController = FindAnyObjectByType<PlayerController>();
        _modosAnimator = Personagem.GetComponent<Animator>();

    }

    public void ChangeModo(int i)
    {
        switch (i)
        {
            case 0:
                _modosAnimator.runtimeAnimatorController = _playerModosController[0]; break;
            case 1:
                _modosAnimator.runtimeAnimatorController = _playerModosController[1]; break;
            case 2:
                _modosAnimator.runtimeAnimatorController = _playerModosController[2]; break;
            case 3:
                _modosAnimator.runtimeAnimatorController = _playerModosController[3]; break;

        }
    }

    public void EnableSkill(int EnabledSkill)
    {
        for (int i = 0; i < EnabledSkill; i++)
        {
            if (_morseCodesToEnable[i].gameObject.activeInHierarchy) return;
            else _morseCodesToEnable[i].gameObject.SetActive(true);
        }
    }

    public void MorseCode(char morse)
    {
        if (_morseIndex < 3)
        {
            morseElements[_morseIndex].gameObject.SetActive(true);
            switch (morse)
            {
                case '.':
                    morseElements[_morseIndex].sprite = MorseSprites[0];
                    break;
                case '-':
                    morseElements[_morseIndex].sprite = MorseSprites[1];
                    break;
            }
        }
    }
}
