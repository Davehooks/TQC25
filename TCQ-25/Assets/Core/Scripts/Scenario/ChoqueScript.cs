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
    float playerSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Entered)
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            PlayerAnim = playerController.gameObject.GetComponent<Animations>();
            dialogEnemy.getPlayerController(playerController);
            playerController.SwitchMode(PlayerController.ModeState.Attack);
            StartCoroutine(WaitForHit());
            UIManager.UImanagerInstance.ModoHud(3);
            Entered = true;
            BalaoFala.SetActive(true);
            playerController.Speed = 0;
            Entered = true;
        }
    }

    public void Interact(InputAction.CallbackContext input)
    {
        if (BalaoFala.activeInHierarchy && input.performed)
        {
            dialogEnemy.NextPhrase();
        }
    }

    IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(1f);
        PlayerAnim.PlayDamage(false);
        if(BalaoFala.activeInHierarchy)
        {
            StartCoroutine(WaitForHit());
        }

    }
}
