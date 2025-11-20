using UnityEngine;
using System.Collections;

public class AttackMode : BasePlayerMode
{
    private GameObject laserPrefab;
    private Transform firePoint;
    private int comboStep = 0;
    private bool canShootLaser = true;
    private float laserCooldown = 1.5f;
    
    

    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);        
        LoadReferences();
        base.playerSFX.PlayTrocarModo();
    }

    private void LoadReferences()
    {
        if (laserPrefab == null)
        {
            laserPrefab = Resources.Load<GameObject>("Core/Prefabs/Player/Laser");
            if (laserPrefab != null)
            {
                Debug.Log("Laser carregado da pasta");
            }
        }
              
        if (laserPrefab == null)
        {
            Debug.LogError("Laser prefab não encontrado'");
            return;
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
        fp.transform.localPosition = new Vector3(0.8f, 0.3f, 0f);
        firePoint = fp.transform;
        Debug.Log("FirePoint criado automaticamente");
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