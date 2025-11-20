using UnityEngine;

public class DefenseMode : BasePlayerMode
{

    private bool isBlocking = false;
    private bool canBlock = true;
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        player.Speed = player.Speed * 0.8f;
        player.JumpForce = player.JumpForce * 0.8f;
        Debug.Log("Defense Mode Ativado");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        player.Speed = player._baseSpeed;
        player.JumpForce = player._baseJumpForce;
        Debug.Log("Defense Mode Desativado");
    }

     public override void HandleAction1()
    {
        Reflect();
    }

     public override void HandleAction2()
    {
        if (!isBlocking && canBlock)
        {
            Block();
        } else if (isBlocking)
        {
            stopBlock();
        } else{
            Debug.Log("Block em cooldown");   
        }
        
    }

    private void Reflect()
    {
        player._anim.PlayAction1();
    }

    private void Block()
    {
        isBlocking = true;
        player._anim.PlayAction2();
    }

    private void stopBlock()
    {
        isBlocking = false;
    }
}