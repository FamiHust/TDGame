using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health Instance { get; private set;}

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
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