using UnityEngine;

public class MeleeAtt : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            Debug.Log($"melee acertou: {enemy.name}");
            enemy.TakeDamage(damage);
        }
    }

    public void EnableAttack()
    {
        col.enabled = true;
    }

    public void DisableAttack()
    {
        col.enabled = false;
    }
}

