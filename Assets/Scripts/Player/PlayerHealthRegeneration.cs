using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerHealthRegeneration : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private float regenInterval = 1f;
    private float elapsed = 0f;

    private void Awake() => playerHealth = GetComponent<PlayerHealth>();

    private void Update()
    {
        if (playerHealth.CurrentHealth < playerHealth.maxHealth)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= regenInterval)
            {
                playerHealth.Heal(1f);
                elapsed = 0f;
            }
        }
        else
        {
            elapsed = 0f;
        }
    }
}
