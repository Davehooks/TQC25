using UnityEngine;
using System.Collections;

public class DefenseMode : BasePlayerMode
{
    private bool isBlocking = false;
    private bool canBlock = true;
    private bool canReflect = true;
    private float reflectCooldown = 1f;
    private float blockCooldown = 0.5f;

    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        player.Speed = player._baseSpeed;
        player.JumpForce = player._baseJumpForce;
        Debug.Log("Defense Mode Ativado");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        if (isBlocking)
        {
            StopBlock();
        }
        player.Speed = player._baseSpeed;
        player.JumpForce = player._baseJumpForce;
        Debug.Log("Defense Mode Desativado");
    }

    public override void HandleAction1()
    {
        if (canReflect)
        {
            Reflect();
        }
        else
        {
            Debug.Log("Reflect cooldown");
        }
    }

    public override void HandleAction2()
    {
        if (!isBlocking && canBlock)
        {
            Block();
        }
        else if (isBlocking)
        {
            StopBlock();
        }
        else
        {
            Debug.Log("Block cooldown");   
        }
    }

    private void Reflect()
    {
        player._anim.PlayAction1();
        
        Debug.Log("Reflect executado");
        
        ReflectNearbyProjectiles();
        
        canReflect = false;
        player.StartCoroutine(ReflectCooldownCoroutine());
    }

    private void Block()
    {
        isBlocking = true;

        
        player._anim.PlayAction2();
        
        Debug.Log("Block ativado");
    }

    private void StopBlock()
    {
        isBlocking = false;
  
        Debug.Log("Block desativado");
        
        canBlock = false;
        player.StartCoroutine(BlockCooldownCoroutine());
    }

    private void ReflectNearbyProjectiles()
    {
        Projectile[] projectiles = GameObject.FindObjectsByType<Projectile>(FindObjectsSortMode.None);
        
        float reflectRadius = 1.2f;
        
        Vector2 reflectDirection = player.IsFacingRight ? Vector2.right : Vector2.left;
        
        int reflectedCount = 0;
        
        foreach (Projectile projectile in projectiles)
        {
            if (projectile == null) continue;
            
            Vector2 directionToProjectile = (projectile.transform.position - player.transform.position).normalized;
            float distance = Vector2.Distance(player.transform.position, projectile.transform.position);
            
            bool isInDistance = distance <= reflectRadius;
            bool isInFront = Vector2.Dot(reflectDirection, directionToProjectile) > 0.3f;
            
            if (isInDistance && isInFront)
            {
                projectile.Reflect(player.gameObject);
                reflectedCount++;
                Debug.Log($"Bala refletida Distância: {distance}");
            }
        }
        
        Debug.Log($"Balas refletidas: {reflectedCount}");
    }


    public void OnProjectileHit(GameObject projectile)
    {
        if (isBlocking)
        {
            GameObject.Destroy(projectile);
            Debug.Log("Bala destruída");
            
        }
    }
        public override void UpdateAnimations()
    {
        if (player == null) return;
        
        float reflectRadius = 3f;
        Vector2 reflectDirection = player.IsFacingRight ? Vector2.right : Vector2.left;
        
        DrawDebugCircle(player.transform.position, reflectRadius, 12, Color.yellow);
        
        Debug.DrawRay(player.transform.position, reflectDirection * reflectRadius, Color.red);
    }

    private IEnumerator ReflectCooldownCoroutine()
    {
        yield return new WaitForSeconds(reflectCooldown);
        canReflect = true;
        Debug.Log("Reflect pronto");
    }

        private void DrawDebugCircle(Vector2 center, float radius, int segments, Color color)
    {
        float angle = 0f;
        float angleIncrement = 360f / segments;
        
        for (int i = 0; i < segments; i++)
        {
            Vector2 start = center + new Vector2(
                Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
                Mathf.Sin(Mathf.Deg2Rad * angle) * radius
            );
            
            Vector2 end = center + new Vector2(
                Mathf.Cos(Mathf.Deg2Rad * (angle + angleIncrement)) * radius,
                Mathf.Sin(Mathf.Deg2Rad * (angle + angleIncrement)) * radius
            );
            
            Debug.DrawLine(start, end, color);
            angle += angleIncrement;
        }
    }

    private IEnumerator BlockCooldownCoroutine()
    {
        yield return new WaitForSeconds(blockCooldown);
        canBlock = true;
    }
}