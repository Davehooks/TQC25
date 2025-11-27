using UnityEngine;

public class Morte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Alguem entrou");
 
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.CurrentHealth = 0;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Alguem entrou");
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.CurrentHealth = 0;
        }
    }
}
