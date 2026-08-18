using UnityEngine;
using System.Collections;

public class AttackMode : BasePlayerMode
{
    private GameObject laserPrefab;
    private Transform firePoint;
    private bool canShootLaser = true;
    private float laserCooldown = 1.5f;
    public MeleeAtt melee;
    
    private bool canMelee = true;
    private float meleeCooldown = 0.5f;
    private float meleeDuration = 1f;

    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);        
        LoadReferences();
        player.Speed = player._baseSpeed;
        melee.DisableAttack();
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

        if (melee == null)
{
    Transform meleeTransform = player.transform.Find("MeleeCollider");
    if (meleeTransform != null)
    {
        melee = meleeTransform.GetComponent<MeleeAtt>();
        if (melee == null)
            Debug.LogError("MeleeCollider encontrado sem script");
    }
    else
    {
        Debug.LogError("Nenhum MeleeCollider encontrado");
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
        if (canMelee)
        {
            MeleeAttack();
        }

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
        if(melee == null)
        {
            Debug.Log("Nao achou collider");
        }
        
        player._anim.PlayAction1();
        base.playerSFX.PlayMeleeAttack();

        melee.EnableAttack();
        Debug.Log("Deu soco");
        
        canMelee = false;
        player.StartCoroutine(MeleeCooldown());
        player.StartCoroutine(DisableMeleeCollider());
        
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
    
    private IEnumerator DisableMeleeCollider()
    {
        yield return new WaitForSeconds(meleeDuration);
        melee.DisableAttack();

    }
    
    private IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;
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