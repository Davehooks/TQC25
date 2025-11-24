using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CallHud : MonoBehaviour
{
    //variáveis

    [SerializeField] private Sprite[] _vidas; // sprites de vida cheia e 
    [SerializeField] private RuntimeAnimatorController[] _playerModosController; // Animator da tela do robô
    [SerializeField] private Animator _modosAnimator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Sprite[] MorseSprites, modoSprites;

    [Header("Referencias da HUD")]
    [SerializeField] private Image _vidaGO;
    [SerializeField] private GameObject vidaParent;
    [SerializeField] private Image[] _morseCodesToEnable;
    [SerializeField] private int _morseIndex = 0;
    [SerializeField] private Image morse;
    [SerializeField] private Image[] morseElements;
    [SerializeField] private Image modo, Personagem;
    

    private List<Image> _vidasTotais = new List<Image>();
    private List<Animator> _vidaAnimator = new List<Animator>();

    [SerializeField] private GameObject gameObjectAtivo;


    private void Start()
    {

        _morseIndex = 0;
        _playerController = FindAnyObjectByType<PlayerController>();
        _modosAnimator = Personagem.GetComponent<Animator>();
        _modosAnimator.SetInteger("Life", _playerController.CurrentHealth);
    }

    public void ChangeModo(int i)
    {
        switch (i)
        {
            case 0:
                _modosAnimator.runtimeAnimatorController = _playerModosController[0];
                modo.sprite = modoSprites[i];
                 break;
            case 1:
                _modosAnimator.runtimeAnimatorController = _playerModosController[1];
                modo.sprite = modoSprites[i];
                break;
            case 2:
                _modosAnimator.runtimeAnimatorController = _playerModosController[2];
                modo.sprite = modoSprites[i];
                break;
            case 3:
                _modosAnimator.runtimeAnimatorController = _playerModosController[3];
                modo.sprite = modoSprites[i];
                break;

        }
    }

    public void EnableSkill(int EnabledSkill)
    {
        for (int i = 0; i < EnabledSkill; i++)
        {
            if (_morseCodesToEnable[i].gameObject.activeInHierarchy) return;
            else _morseCodesToEnable[i].gameObject.SetActive(true);
        }
        morse.gameObject.SetActive(true);
    }

    public void MorseCode(char morseChar)
    {
        if (!morse.gameObject.activeInHierarchy)
        {
            morse.gameObject.SetActive(true);
        }
        Animator morseAnim = morse.gameObject.GetComponent<Animator>();
        morseAnim.SetTrigger("FadeIn");
        if (_morseIndex < 3)
        {
            morseElements[_morseIndex].gameObject.SetActive(true);
            if (morseChar == '.')
            {
                morseElements[_morseIndex].sprite = MorseSprites[0];
            }
            else if (morseChar == '-')
            {
                morseElements[_morseIndex].sprite = MorseSprites[1];
            }
            _morseIndex++;
        }
        else { _morseIndex = 0; }
    }

    public void ZerarMorse()
    {
        Animator morseAnim = morse.gameObject.GetComponent<Animator>();

        for (int i = 0;i < morseElements.Length;i++)
        {
            morseElements[i].gameObject.SetActive(false);
        }
        _morseIndex = 0;
        morseAnim.SetTrigger("FadeOut");
        
    }

    public void VidaPersonagemHUD()
    {
        if (_modosAnimator.GetInteger("Life") > _playerController.CurrentHealth)
        {
            _modosAnimator.SetTrigger("Damage");
        }
        _modosAnimator.SetInteger("Life",_playerController.CurrentHealth);
        BarraVidaHUD();
    }

    public void InstanciarVida()
    {
        _vidasTotais.Capacity = _playerController.MaxHealth;
        _vidaAnimator.Capacity = _playerController.MaxHealth;
        _vidasTotais.Add(_vidaGO);
        _vidaAnimator.Add(_vidaGO.gameObject.GetComponent<Animator>());
        _vidaAnimator[0].SetInteger("Posicao", 2);

        for (int i = 1; i < _playerController.MaxHealth; i++)
        {
            Image novaVida = Instantiate(_vidaGO);
            novaVida.transform.SetParent(vidaParent.transform);
            novaVida.GetComponent<RectTransform>().localPosition = new Vector3(0, -42 + _vidasTotais[i-1].GetComponent<RectTransform>().localPosition.y, 0);
            _vidasTotais.Add(novaVida);
            novaVida.GetComponent<RectTransform>().localScale = Vector3.one*7;
            _vidaAnimator.Add(_vidasTotais[i].gameObject.GetComponent<Animator>());
          if(i != _playerController.MaxHealth)
          {
                _vidaAnimator[i].SetInteger("Posicao", 1);
          }
          if (i == _playerController.MaxHealth -1)
            {
                _vidaAnimator[i].SetInteger("Posicao", 0);
            }
        }
        BarraVidaHUD();
    }

    public void BarraVidaHUD()
    {
        for (int i = 0; i<=_playerController.MaxHealth-1 ;i++)
        {

            if (_playerController.MaxHealth - i > _playerController.CurrentHealth) // i tá sem vida
            {
                _vidaAnimator[i].SetInteger("Life", -1);

            }
            else if(_playerController.MaxHealth - i == _playerController.CurrentHealth) // i ta na vida
            {
                _vidaAnimator[i].SetInteger("Life", 0);

            }
            else if (_playerController.MaxHealth - i < _playerController.CurrentHealth) // i ta com vida
            {
                _vidaAnimator[i].SetInteger("Life", 1);
            }
            else
            {
                Debug.Log($"Deu errado {i} vezes");
            }
        }
    }

}
