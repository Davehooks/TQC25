using UnityEngine;

public abstract class BasePlayerMode : IPlayerMode
{
    protected PlayerController player;
    
    public virtual void EnterMode(PlayerController player)
    {
        this.player = player;
    }
    
    public virtual void ExitMode() { }
    
    public virtual void HandleMovement(Vector2 moveInput)
    {
        if (player.IsCrouching) 
        {
            player.Rigidbody.linearVelocity = new Vector2(
                moveInput.x * player.Speed * player.CrouchSlow * Time.deltaTime,
                player.Rigidbody.linearVelocity.y
            );
        }
        else
        {
            player.Rigidbody.linearVelocity = new Vector2(
                moveInput.x * player.Speed * Time.deltaTime,
                player.Rigidbody.linearVelocity.y
            );
        }
    }
    
    public virtual void HandleJump()
    {
        player.Rigidbody.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
        player.IsGrounded = false;
    }
    
    public virtual void HandleAction1() { }
    public virtual void HandleAction2() { }
    
    public virtual void HandleCrouch()
    {
        player.IsCrouching = !player.IsCrouching;
    }
    
    public virtual void UpdateAnimations() { }
}