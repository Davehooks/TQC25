using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RedHood : Enemy, IDamageable
{
    [Header("RedHood Variabels")]

    [SerializeField] private Transform groundCheck; //detecta o chão
    [SerializeField] private Transform[] visionCheck = new Transform[2];
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float checkRadius = 0.1f; //bem baixo para ser preciso
    
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private float shootingTime = 0.2f;
    [SerializeField] private GameObject _prefabProjectile;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool isShooting = false;

    [Range(-25f, 45f)]public float shootingAngle = 0f;
    [Header("Vida")]
    [SerializeField] private int enemyCurrentHealth = 3;
    [SerializeField] private int enemyMaxHealth = 3;

    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        Debug.Log($"RedHood iniciado com {enemyCurrentHealth} de vida");
    }
    //Getter / Setter
    public bool IsFacingRight()
    {
        return isFacingRight;
    }



    //Override methods
    //RedHood vai se mover observando plataformas
    void Update()
    {
        VisionChecking();
    }
    
    public override void Move()
    {
        if (!isShooting)
        {
            float direction = isFacingRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            //se está atirando não anda
            rb.linearVelocityX = 0;
        }
    }
    public void TakeDamage(int amount, GameObject source = null)
    {
        Debug.Log($"Redhood: TakeDamage chamado Dano: {amount}, Vida antes: {enemyCurrentHealth}");
        
        if (enemyCurrentHealth <= 0)
        {
            Debug.Log("RedHood morto");
            return;
        }
        
        enemyCurrentHealth -= amount;
        Debug.Log($"Vida inimigo: {enemyCurrentHealth}");
        
        if (enemyCurrentHealth <= 0)
        {
            Debug.Log("Redhood morreu");
            Die();
        }
    }

    protected override void OnHitAnimation(int amountDamage, GameObject source)
    {
        base.OnHitAnimation(amountDamage, source);
        Debug.Log($"{name} foi atingido Vida atual: {enemyCurrentHealth}");
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log($"{name} morreu!");
    }

    //Metodos
    private void VisionChecking()
    {
        //checks if the mob sees the player
        if (Physics2D.Linecast(visionCheck[0].position, transform.position, playerLayer) || Physics2D.Linecast(visionCheck[1].position, transform.position, playerLayer))
        {
            if (canShoot)
                Atack();
        }
        //checks if the mob sees the border of the tilemap
        if (!Physics2D.Linecast(groundCheck.position, transform.position, groundLayer))
        {
            Flip();
        }
    }
    private void Atack()
    {
        StartCoroutine(Shooting(shootingTime));
    }



    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    //VISUALIZACAO NO EDITOR
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, groundCheck.position);
        }
        if (visionCheck != null)
        {
            Gizmos.color = Color.aliceBlue;
            Gizmos.DrawLine(transform.position, visionCheck[0].position);
            Gizmos.DrawLine(transform.position, visionCheck[1].position);
        }
    }
    //COROUTINES
    public IEnumerator Shooting(float timing)
    {
        //TODOOOOO
        isShooting = true;
        canShoot = false;
        Debug.Log("REDHOOD: Instanciei uma bala");
        GameObject projectileObj = Instantiate(_prefabProjectile, weaponPosition.position, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();

        if(projectile != null)
        {
            projectile.SetShooter(this);
        }

        yield return new WaitForSeconds(timing);
        isShooting = false;
        canShoot = true;
    }
}