using UnityEngine;
using System.Collections;

public class AttackMode : BasePlayerMode
{
    private GameObject laserPrefab;
    private Transform firePoint;
    private bool canShootLaser = true;
    private float laserCooldown = 1.5f;
    public Collider2D meleeCollider;
    

    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);        
        LoadReferences();
        player.Speed = player._baseSpeed;
        meleeCollider.enabled = false;
        player.JumpForce = player._baseJumpForce;
        base.playerSFX.PlayTrocarModo();
    }

    private void LoadReferences()
    {
        if (laserPrefab == null)
        {
            laserPrefab = Resources.Load<GameObject>("Prefabs/Laser");
            if (laserPrefab != null)
            {
                Debug.Log("Laser carregado da pasta");
            }
        }
              
        if (firePoint == null)
        {
            firePoint = player.transform.Find("FirePoint");
            if (firePoint == null)
            {
                CreateFirePoint();
            }
        }
    }
    
    private void CreateFirePoint()
    {
        GameObject fp = new GameObject("FirePoint");
        fp.transform.SetParent(player.transform);
        fp.transform.localPosition = new Vector3(1f, 0.5f, 0f);
        firePoint = fp.transform;
        Debug.Log("FirePoint criado");
    }
    
    public override void HandleAction1()
    {
        MeleeAttack();
        base.playerSFX.PlayMeleeAttack();

    }
    
    public override void HandleAction2()
    {
        if (canShootLaser && laserPrefab != null)
        {
            ShootLaser();
        }
        else if (!canShootLaser)
        {
            Debug.Log("Laser em cooldown");
        }
        else
        {
            Debug.LogError("Laser prefab não configurado");
        }
    }
    
    private void MeleeAttack()
    {
        player._anim.PlayAction1();
        base.playerSFX.PlayMeleeAttack();


        meleeCollider.enabled = true;
        
    }
    
    private void ShootLaser()
    {
        player._anim.PlayAction2();
        base.playerSFX.PlayRangedAttack();
        
        GameObject laser = GameObject.Instantiate(
            laserPrefab, 
            firePoint.position, 
            Quaternion.identity
        );
        
        LaserProjectile laserScript = laser.GetComponent<LaserProjectile>();
        if (laserScript != null)
        {
            laserScript.Initialize(player.IsFacingRight);
        }
        
        canShootLaser = false;
        player.StartCoroutine(LaserCooldown());
        
        Debug.Log("laser disparado");
    }
    
    private IEnumerator LaserCooldown()
    {
        yield return new WaitForSeconds(laserCooldown);
        canShootLaser = true;
        Debug.Log("Laser pronto");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        if (player != null) 
        {
            player.StopAllCoroutines();
            canShootLaser = true;
        }
    }
}