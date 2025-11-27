using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float speed = 7f;
    [SerializeField] public int damage = 1;
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private RedHood _shooter;
    [SerializeField] private SpriteRenderer __shooterRenderer;
    private GameObject originalShooter;
    private bool isReflected = false;
    private Animator _anim;

    public void SetShooter(RedHood shooter)
    {
        this._shooter = shooter;
        this.originalShooter = shooter.gameObject;
    }

    private void Start()
    {
        if(_anim == null) 
            _anim = GetComponent<Animator>();
        if (_shooter == null)
            _shooter = GameObject.FindFirstObjectByType<RedHood>();
        if (__shooterRenderer == null)
            __shooterRenderer = GetComponent<SpriteRenderer>();
        if(_shooter.IsFacingRight())
            __shooterRenderer.flipX = true;
        float weaponDirection = _shooter.IsFacingRight() ? 1f : -1f; 
        
        Vector2 direction = CreateVector2ByDegree(_shooter.shootingAngle);
        velocity = direction * speed * weaponDirection;
        originalShooter = _shooter.gameObject;
        transform.rotation = Quaternion.Euler(0f, 0f, _shooter.shootingAngle);
        
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



    //transforma o angulo em graus para radiandos
    private Vector2 CreateVector2ByDegree(float degree)
    {
        //vamos criar uma variavel para receber o angulo já em radiandos(que é o que a unity usa)
        float angle = degree * Mathf.Deg2Rad;
        //resolve os valores para nosso vetor; então x está para cos e y está para sin
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}