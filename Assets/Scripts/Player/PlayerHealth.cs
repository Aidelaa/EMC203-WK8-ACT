using System;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float CurrentHealth { get; private set; }
    public UnityEvent<float> OnHealthChanged = new();

    private void Awake() => CurrentHealth = maxHealth;

    public void Damage(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, maxHealth);
        OnHealthChanged.Invoke(CurrentHealth);
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
        OnHealthChanged.Invoke(CurrentHealth);
    }
}
