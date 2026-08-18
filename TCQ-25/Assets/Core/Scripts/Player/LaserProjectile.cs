using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private int damage = 1;
    
    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    public void Initialize(bool isFacingRight)
    {
        direction = isFacingRight ? Vector2.right : Vector2.left;
        
        if (!isFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            DestroyLaser();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroyLaser();
        }
    }

    private void DestroyLaser()
    {
        Destroy(gameObject);
    }
}