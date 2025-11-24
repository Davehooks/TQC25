using UnityEngine;
using System.Collections;

public class AgilityMode : BasePlayerMode
{
    private bool canDash = true;
    private float dashCooldown = 2f;
    private float dashSpeed = 30f;
    private float dashDuration = 0.3f;
    private bool isDashing = false;
    private Vector2 dashDirection;
    
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        player.Speed = player._baseSpeed;
        player.JumpForce = player._baseJumpForce* 1.5f;
        Debug.Log($"Modo Agilidade ativado - Velocidade: {player.Speed}");
        base.playerSFX.PlayTrocarModo();

    }

    public override void ExitMode()
    {
        base.ExitMode();
        
        player.Speed = player._baseSpeed;
        player.Speed = player._baseJumpForce;
        if (player != null) 
        {
            player.StopAllCoroutines();
            isDashing = false;
            canDash = true;
        }
    }
    
    public override void HandleAction1()
    {
        if (canDash && !isDashing && player != null && !player.IsBeingHit)
        {
            Dash();
        }
    }
    
    public override void HandleAction2()
    {

    }
    
    private void Dash()
    {
        isDashing = true;
        canDash = false;
        
        if (player.Animator != null)
        {
            base.playerSFX.PlayDash();
            player._anim.PlayAction1();
            Debug.Log("Animação Dash acionada");
        }
        else
        {
            Debug.LogWarning("Animator não encontrado no player");
        }
        
        dashDirection = GetDashDirection();
        player.Rigidbody.linearVelocity = dashDirection * dashSpeed;
        
        player.StartCoroutine(DashCoroutine());
    }
    
    private Vector2 GetDashDirection()
    {
        if (Mathf.Abs(player.Rigidbody.linearVelocity.x) > 0.1f)
        {
            return new Vector2(Mathf.Sign(player.Rigidbody.linearVelocity.x), 0).normalized;
        }
        return player.IsFacingRight ? Vector2.right : Vector2.left;
    }
    
    private IEnumerator DashCoroutine()
    {
        float timer = 0f;
        while (timer < dashDuration && isDashing)
        {
            player.Rigidbody.linearVelocity = dashDirection * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }
        
        EndDash();
        
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        ResetDash();
    }
    
    private void EndDash()
    {
        isDashing = false;
        if (player.Rigidbody != null)
        {
            player.Rigidbody.linearVelocity = dashDirection * dashSpeed * 0.3f;
        }
    }
    
    private void ResetDash()
    {
        canDash = true;
        Debug.Log("Dash disponível");
    }
    
    public override void HandleMovement(Vector2 moveInput)
    {
        if (isDashing) return;
        base.HandleMovement(moveInput);
    }
}