using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerMode
{
    void EnterMode(PlayerController player);
    void ExitMode();
    void HandleMovement(Vector2 moveInput);
    void HandleJump();
    void HandleAction1();
    void HandleAction2();
    void HandleCrouch();
    void UpdateAnimations();
}