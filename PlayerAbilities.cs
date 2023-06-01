using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    private PlayerInput pInput;
    public List<Projectile> projectiles;
    public List<Projectile> freezeProjectiles;
    public Transform cannon;
    public float rateOfFire = 0.1f;
    public float rateOfFireFreezeLasers = 0.2f;
    private bool bFire;
    int pCounter = 0;

    private bool bToggleProjectileType = true; 

    private bool bSwitchProjectileType;
    private void Awake()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        pInput.Player.Fire.started += Fire;
        pInput.Player.Fire.canceled += StopFiring;
        GlobalResetGA.ResetWorld += ResetProjectilese;
        EnableFreezeProjectilesGA.SwitchToFreeze += SwitchProjectileFiringType;
    }

    private void OnDisable()
    {
        pInput.Player.Fire.started -= Fire;
        pInput.Player.Fire.canceled -= StopFiring;
        GlobalResetGA.ResetWorld -= ResetProjectilese;
        EnableFreezeProjectilesGA.SwitchToFreeze -= SwitchProjectileFiringType;
    }

    private void SwitchProjectileFiringType(bool value)
    {
        
        bSwitchProjectileType = value;

        if(bToggleProjectileType == true)
        {
            StopCoroutine(nameof(FiringStandard));
            StartCoroutine(nameof(FiringFreeze));
            bToggleProjectileType = false;
        }
        else
        {
            StopCoroutine(nameof(FiringFreeze));
            StartCoroutine(nameof(FiringStandard));
            bToggleProjectileType = true;
        }
        
    }

    private void Fire(InputAction.CallbackContext fireDaBullet)
    {
        if (bFire) return; // prevent starting another coroutine if fire is already running.
        bFire = true;

        if (bSwitchProjectileType == false)
        {
            StartCoroutine(nameof(FiringStandard));
        }
            
        else
            StartCoroutine(nameof(FiringFreeze));
    }

    private void StopFiring(InputAction.CallbackContext haltFiring)
    {
        bFire = false;
    }


    private void ResetProjectilese() // disable player from shooting during reset sequence
    {
        StopAllCoroutines();
        foreach (Projectile item in projectiles)
            item.DisableProjectile();

        foreach (Projectile item in freezeProjectiles)
            item.DisableProjectile();

        bFire = false;
    }

   
    IEnumerator FiringStandard()
    {
        while(bFire)
        {
            if (pCounter >= projectiles.Count)
                pCounter = 0;

               projectiles[pCounter].transform.position = cannon.position;
               projectiles[pCounter].EnableProjectile();
               pCounter++;
            
            yield return new WaitForSeconds(rateOfFire);
        }
    }

    IEnumerator FiringFreeze()
    {
        while (bFire)
        {
            if (pCounter >= freezeProjectiles.Count)
                pCounter = 0;

            freezeProjectiles[pCounter].transform.position = cannon.position;
            freezeProjectiles[pCounter].EnableProjectile();
            pCounter++;

            yield return new WaitForSeconds(rateOfFireFreezeLasers);
        }
    }
}
