using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChoqueScript : MonoBehaviour
{

    private PlayerController playerController;
    private Animations PlayerAnim;
    [SerializeField] private GameObject BalaoFala;
    [SerializeField] private bool Entered = false;
    [SerializeField] private DialogEnemy dialogEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            PlayerAnim = playerController.gameObject.GetComponent<Animations>();
            dialogEnemy.getPlayerController(playerController);
            playerController.SwitchMode(PlayerController.ModeState.Attack);
            StartCoroutine(WaitForHit());
            UIManager.UImanagerInstance.ModoHud(3);
            Entered = true;
            BalaoFala.SetActive(true);
            
            if(BalaoFala.activeInHierarchy)
            {
            playerController.Speed = 0;
            }
            else
            {
                playerController.Speed = playerController._baseSpeed;
                Debug.Log($"{playerController.Speed}");
            }
        }
    }

    public void Interact(InputAction.CallbackContext input)
    {
        if (BalaoFala.activeInHierarchy && input.performed)
        {
            Debug.Log("Era pra chamar o prox dialogo");
            dialogEnemy.NextPhrase();
        }
    }

    IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnim.PlayDamage();
        if(BalaoFala.activeInHierarchy)
        {
            StartCoroutine(WaitForHit());
        }

    }
}
