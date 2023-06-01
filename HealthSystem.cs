using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private int currentHealth;

    public static Action<float> UpdateHealthUI = delegate { };
    public List<GameActions> deathActionList; 
    public List<GameActions> respawnActionList; 
    public bool bPlayer;
    private bool bInvincible;

    public int lives = 1;
    private int defaultLives;

    [SerializeField]private bool bBossEnemy = false;

    private void Awake()
    {
        defaultLives = lives;
    }

    private void OnEnable() // Establish communications with other aspects of the Health System
    {
        HealthPickupGA.IncreaseHealth += UpdateHealth;
        InvincibilityGA.InvincibilityOn += InvincibilityOn;
        InvincibilityGA.InvincibilityOff += InvincibilityOff;
        GlobalResetGA.ResetWorld += ResetHealthSystem;
           
    }
    private void Start()
    {
        if(bPlayer)
        UpdateHealthUI(currentHealth / maxHealth);
    }
    private void OnDisable()
    {
        HealthPickupGA.IncreaseHealth -= UpdateHealth;
        InvincibilityGA.InvincibilityOn -= InvincibilityOn;
        InvincibilityGA.InvincibilityOff -= InvincibilityOff;
        GlobalResetGA.ResetWorld -= ResetHealthSystem;
    }

    public void UpdateHealth(int incrementAmount)
    {
        if (bInvincible) return;
        currentHealth = Mathf.Clamp(currentHealth + incrementAmount, 0, (int)maxHealth);
        // The line above is equivalent to the following lines below
        // currentHealth += incrementAmount;
        //currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(bPlayer)
            UpdateHealthUI(currentHealth / maxHealth);

        if(currentHealth == 0 && lives > 0) // death actions
        {
            lives--;
            if(lives == 0)
            {
                foreach (GameActions item in deathActionList)
                    item.Action();
            } 
            else
            {
                foreach (GameActions item in respawnActionList)
                    item.Action();
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out DamageVolume dVol))
        {
            UpdateHealth(dVol.Damage);
        }
        else if (other.TryGetComponent(out HealthVolume hVol))
        {
            UpdateHealth(hVol.HealthIncrease);
        }
    }

    public void RestoreHealth()
    {
        currentHealth = (int)maxHealth;
        if (bPlayer) // only the player should send info to the health bar UI
            UpdateHealthUI(1); // 1 = max health for bar
    }

    private void InvincibilityOn()
    {
        bInvincible = true;
    }

    private void InvincibilityOff()
    {
        bInvincible = false;
    }

    public void ResetHealthSystem() // function called when game is reset
    {
        if (bBossEnemy) return;
        currentHealth = (int)maxHealth;
        lives = defaultLives;
    }
}
