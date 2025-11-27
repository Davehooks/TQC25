using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RedHood : Enemy
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

    private void Start()
    {
        animator = GetComponent<Animator>();
        
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
        animator.SetTrigger("Damage");
        
        base.TakeDamage(amount, source);
    }

    protected override void OnHitAnimation(int amountDamage, GameObject source)
    {
        base.OnHitAnimation(amountDamage, source);
    }

    protected override void Die()
    {
        animator.SetBool("Death",true);
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
            animator.SetTrigger("Shoot");
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
        isShooting = true;
        canShoot = false;
        
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

    public void Death()
    {
        Destroy(gameObject);
    }
    public void CantShoot()
    {
        this.gameObject.tag = "Untagged";
        canShoot = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ReflectedProjectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage, collision.gameObject);
            }
            
        }
    }
}