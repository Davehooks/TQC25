using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollider : MonoBehaviour
{
    public LayerMask groundLayer;
    [SerializeField] private BoxCollider2D feetCollider;
    [SerializeField] private BoxCollider2D bodyCollider;
    [SerializeField] private PlayerController playerController;
    private PlayerSFX soundScript;
    [SerializeField] private float _bounceY = 5.0f;
    [SerializeField] private float _bounceX = 3.5f;
    
    private Rigidbody2D _rb;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        soundScript = playerController.gameObject.GetComponent<PlayerSFX>();
    }
    //se tem colisão estou lidando com o corpo
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("COLLIDER: Perdeu uma vida no Collision");
            playerController.TakeDamage(1);
            QuicaQuica(false);
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            playerController.TakeDamage(1);
            QuicaQuica(true);
        }
    }
    //se tem trigger estou lidando com o pé
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o que colidiu é um inimigo que implementa IDamageable
        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();

        
        if (enemy != null && collision.CompareTag("Enemy"))
        {
            //Todo -- Se ele não for null dá dano no inimigo, quica pra cima pelo impacto

            //aqui dá dano no inimigo indiferente de qual seja pela interface
            enemy.TakeDamage(1, this.gameObject);
            QuicaQuica(true);
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if(!playerController.IsGrounded)
            {
                soundScript.PlayDrop();
            }

            playerController.IsGrounded = true;
        }
        
    }
    

    public void QuicaQuica(bool isEnemy)
    {
        //aqui ele quica pra cima
            _rb.linearVelocity = Vector2.zero;
            _rb.AddForceY(_bounceY, ForceMode2D.Impulse);
            
            if(isEnemy)
        {
            float directionWin = playerController.IsFacingRight ? 1f : -1f;
            _rb.AddForceX(_bounceX/2 * directionWin, ForceMode2D.Impulse);
        }
            float directionLost = playerController.IsFacingRight ? -1f : 1f;
            _rb.AddForceX(_bounceX * directionLost, ForceMode2D.Impulse);

    }
    /* void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Ground"))
         {
             playerController.IsGrounded = true;
         }
     }
     */
}
