using UnityEngine;
using UnityEngine.SceneManagement;

public class CallCreditos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Alguem entrou");
 
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            SceneManager.LoadScene("Creditos");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Alguem entrou");
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            SceneManager.LoadScene("Creditos");
        }
    }
}
