using UnityEngine;

public class DefenseMode : BasePlayerMode
{
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        Debug.Log("Defense Mode Ativado");
        base.playerSFX.PlayTrocarModo();
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
        base.playerSFX.PlayReflect();
    }

     public override void HandleAction2()
    {
        base.playerSFX.PlayBlock();
    }
}