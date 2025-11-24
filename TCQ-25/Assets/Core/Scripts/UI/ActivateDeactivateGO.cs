using UnityEngine;
using UnityEngine.UI;

public class ActivateDeactivateGO : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private Image[] _imagens;
    [SerializeField] private Dialog _dialog;
    [SerializeField] private DialogEnemy _dialogEnemy;
    [SerializeField] private TriggerCapsula trigger;


    //ESSE SCRIPT É USADO PRATICAMENTE PARA EVENT DE ANIMAÇÕES NÃO VAI APARECER QUE TEM REFERÊNCIA, NÃO EXCLUIR, PF D:

    public void DeactivateThisGO()
    {
        this.gameObject.SetActive(false);
    }
    public void DeactivateGO()
    {
        for (int i = 0; i < _gameObject.Length; i++)
        {
            _gameObject[i].SetActive(false);
        }
    }
    public void ActivateGO()
    {
        for (int i = 0; i < _gameObject.Length; i++)
        {
            _gameObject[i].SetActive(true);
        }
    }

    public void ActivateImage() // usado na animação
    {
        for (int i = 0; i < _imagens.Length; i++)
        {
            _imagens[i].enabled = true;
        }
    }

    public void CallDialog() // usado na animação pra chamar o balão de fala
    {
        if (_dialogEnemy != null)
        {
            _dialogEnemy.CallText();
        }
        else if (_dialog != null) 
        {
            _dialog.CallText();

        }
    }

    public void OpenEntire() // Vai ser usado na animação pra aparecer o cientista quando a fumaça subir, mamãe saiu
    {
        trigger._OpenedEntire = true;
    }

    public void DestroyGO()
    {
        if(_gameObject != null)
        {
        for (int i = 0; i < _gameObject.Length; i++)
        {
            Destroy(_gameObject[i]);
        }
        }
    }

    public void InstanciateLifes()
    {
        UIManager.UImanagerInstance.InstanciateVida();
    }
}
