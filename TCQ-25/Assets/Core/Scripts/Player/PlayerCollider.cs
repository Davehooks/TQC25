using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollider : MonoBehaviour
{
    public LayerMask groundLayer;
    [SerializeField] private BoxCollider2D feetCollider;
    [SerializeField] private BoxCollider2D bodyCollider;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float _bounceY = 5.0f;
    [SerializeField] private float _bounceX = 3.5f;
    
    private Rigidbody2D _rb;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    //se tem colisão estou lidando com o corpo
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("COLLIDER: Perdeu uma vida no Collision");
            playerController.TakeDamage(1);
            QuicaQuica();
        }
    }
    //se tem trigger estou lidando com o pé
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o que colidiu é um inimigo que implementa IDamageable
        IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();

        if (enemy != null && collision.CompareTag("Obstacle"))
        {
            Debug.Log("COLLIDER: Perdeu uma vida no Trigger");
            playerController.TakeDamage(1);
            QuicaQuica();
        }
        else if (enemy != null)
        {
            //Todo -- Se ele não for null dá dano no inimigo, quica pra cima pelo impacto

            //aqui dá dano no inimigo indiferente de qual seja pela interface
            enemy.TakeDamage(1, this.gameObject);


        QuicaQuica();
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerController.IsGrounded = true;
        }
        
    }
    

    public void QuicaQuica()
    {
        //aqui ele quica pra cima
            _rb.linearVelocity = Vector2.zero;
            _rb.AddForceY(_bounceY, ForceMode2D.Impulse);

        float direction = playerController.IsFacingRight ? -1f : 1f;
            _rb.AddForceX(_bounceX, ForceMode2D.Impulse);

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
