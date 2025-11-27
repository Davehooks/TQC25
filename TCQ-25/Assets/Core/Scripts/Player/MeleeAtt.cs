using UnityEngine;

public class MeleeAtt : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    private PlayerController player;

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {            
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log($"Acertou o: {collision.gameObject.name}");
                enemy.TakeDamage(damage);
            } else
            {
                Debug.Log("Acertou nao");
            }
        }
    }
}