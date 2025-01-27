using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;
    public TowerBar towerBar;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth); 
        }
    }

    public void TakeDamageTower(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (towerBar != null)
        {
            towerBar.UpdateBar(currentHealth, maxHealth); 
        }
    }

}