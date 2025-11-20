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
    [SerializeField] private Sprite[] MorseSprites, modoSprites;

    [Header("Referencias da HUD")]
    [SerializeField] private GameObject _vidaGO;
    [SerializeField] private GameObject[] _vidasTotais;
    private Animator[] _vidaAnimator;
    [SerializeField] private Image[] _morseCodesToEnable;
    [SerializeField] private int _morseIndex = 0;
    [SerializeField] private Image morse;
    [SerializeField] private Image[] morseElements;
    [SerializeField] private Image modo, Personagem;

    private void Start()
    {

        _morseIndex = 0;
        _playerController = FindAnyObjectByType<PlayerController>();
        _modosAnimator = Personagem.GetComponent<Animator>();
        InstanciarVida();

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
        if (!morse.gameObject.activeInHierarchy) morse.gameObject.SetActive(true);
        if(_morseIndex < 3)
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
        else { Debug.Log("Aqui passou"); }
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
        _modosAnimator.SetInteger("Life",_playerController.CurrentHealth);
        BarraVidaHUD();
    }

    public void InstanciarVida()
    {
        _vidasTotais[0] =_vidaGO;
        _vidaAnimator[0] = _vidaGO.GetComponent<Animator>();
        _vidaAnimator[0].SetInteger("Posicao", 2);
        for (int i = 1; i <= _playerController.MaxHealth; i++)
        {
            _vidasTotais[i] = Instantiate(_vidaGO, _vidasTotais[i-1].transform.position + new Vector3(0, -41, 0), Quaternion.identity);
            _vidaAnimator[i] = _vidasTotais[i].GetComponent<Animator>();
          if(i != _playerController.MaxHealth)
          {
                _vidaAnimator[i].SetInteger("Posicao", 1);
          }
          if (i == _playerController.MaxHealth)
            {
                _vidaAnimator[i].SetInteger("Posicao", 0);
            }
        }
    }

    public void BarraVidaHUD()
    {
        for(int i = 0;i<_playerController.MaxHealth;i++)
        {

            if (i > _playerController.CurrentHealth) // i tá sem vida
            {
                _vidaAnimator[i].SetInteger("Life", -1);
            }

            else if( i ==_playerController.CurrentHealth) // i ta na vida
            {
                _vidaAnimator[i].SetInteger("Life", 0);

            }
            else // i ta com vida
            {
                _vidaAnimator[i].SetInteger("Life", 1);

            }
        }
    }

}
