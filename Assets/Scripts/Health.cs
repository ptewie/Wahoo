using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("health values")]
    public float currentHealth;
    public float maxHealth;
    [SerializeField] private float initialHealth;
    [Header("events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;

    public void Start ()
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnTakeDamage.Invoke();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealDamage (float damage)
    {
        currentHealth += damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHeal.Invoke();
    }

    public void HealToFull ()
    {
        currentHealth = maxHealth;
    }

    public void Die ()
    {
        currentHealth = 0;

        OnDeath.Invoke();
    }

    public float HealthPercent()
    {
        return currentHealth / maxHealth;
    }
}