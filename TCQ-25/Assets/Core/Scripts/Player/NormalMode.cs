using UnityEngine;

public class NormalMode : BasePlayerMode
{
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        base.playerSFX.PlayErrouModo();
        Debug.Log("Normal Mode Ativado");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        Debug.Log("Normal Mode Desativado");
    }

     public override void HandleAction1()
    {
        
    }

     public override void HandleAction2()
    {

    }
}