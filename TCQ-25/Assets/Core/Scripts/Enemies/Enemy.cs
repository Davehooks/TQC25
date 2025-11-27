using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("General Attributes")] //serão protected para poder usar na classe ou nas filhas mas não no resto
    [SerializeField] protected int maxHealth = 1;
    
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float invincibilityTime = 0.5f;

    [Header("Current in-game values")]

    protected int currentHealth;
    protected bool isInvincible;

    [Header("Internal Components")]
    protected Animator animator;
    protected Rigidbody2D rb;

    //Funcoes do sistema
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        // As subclasses podem usar isso para inicialização adicional
    }
    protected virtual void FixedUpdate()
    {
        // Aqui chamamos o movimento — as subclasses definem o que Move faz
        Move();
    }
    public virtual void TakeDamage(int damageAmount, GameObject source = null) //source = null é o default value igual em JS
    {
        if (isInvincible) return; //se estiver invencivel ignora o dano

        currentHealth -= damageAmount;
        OnHitAnimation(damageAmount, source);
        Debug.Log($"ENEMY: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
        else
            StartCoroutine(TemporaryInvincibility());
    }

//Metodos padroes

    protected virtual void Die()
    {
        // Default death behavior
        Destroy(gameObject);
    }

    protected virtual void OnHitAnimation(int damageAmount, GameObject source)
    {
        // Tem o amount caso a gente queira dar feedback diferente dependendo do dano
        if (animator != null)
            animator.SetTrigger("Hit");
    }


    //Metodos abstratos

    public abstract void Move();
    


//Metodos de Coroutine
    /// <summary>
    /// Torna o inimigo invencível por um curto período após ser atingido.
    /// </summary>
    protected virtual IEnumerator TemporaryInvincibility() 
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    
}