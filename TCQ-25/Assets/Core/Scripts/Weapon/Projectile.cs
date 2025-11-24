using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float speed = 7f;
    [SerializeField] public int damage = 1;
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private RedHood _redHood;
    private bool isReflected = false;
    private GameObject originalShooter;

    public int Damage => damage;

    private void Start()
    {
        if(_redHood == null)
            _redHood = GameObject.FindFirstObjectByType<RedHood>();
        
        float direction = _redHood.IsFacingRight() ? 1f : -1f;
        velocity = new Vector2(direction * speed, 0f);
        originalShooter = _redHood.gameObject;
        
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void Reflect(GameObject reflector)
    {
        if (isReflected) return;
    
    isReflected = true;
    
    originalShooter = reflector;
    
    velocity = new Vector2(-velocity.x, 0f);
    
    damage *= 2;
    
    SpriteRenderer sprite = GetComponent<SpriteRenderer>();
    if (sprite != null)
    {
        sprite.color = Color.cyan;
    }
        
        CancelInvoke("Destroy");
        Destroy(gameObject, lifetime);
        
        Debug.Log("Bala refletida");
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log($"Colisao: Bala atingiu {collision.gameObject.name} (Tag: {collision.tag})");
    
    if (collision.gameObject == originalShooter) 
    {
        Debug.Log("Ignorando colisão com atirador original");
        return;
    }
    
    if (isReflected && collision.CompareTag("Enemy"))
    {
        Debug.Log("Bala refletida acertou enemy");
        
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log($"Causando {damage} de dano no inimigo");
            
            damageable.TakeDamage(damage, gameObject);
        }

        
        Destroy(gameObject);
    }
    else if (!isReflected && collision.CompareTag("Player"))
    {
        Debug.Log("Bala acertou player");
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, gameObject);
        }
        Destroy(gameObject);
    }
    else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
        Debug.Log("Bala atingiu o chão ou parede");
        Destroy(gameObject);
    }
    else
    {
        Debug.Log($"Colisão ignorada: {collision.gameObject.name} (Tag: {collision.tag})");
    }
}
}