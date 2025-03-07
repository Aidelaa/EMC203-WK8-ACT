using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerHealthRegeneration : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private float duration = 1f;
    private float elapsed = 0f;
    
    private void Awake()
    {
        TryGetComponent<PlayerHealth>(out this.playerHealth);
    }

    private void Update()
    {
        if (this.playerHealth.currentHealth < this.playerHealth.maxHealth)
        {
            this.elapsed += Time.deltaTime;

            if (this.elapsed >= this.duration)
            {
                this.playerHealth.currentHealth += 1f;
                this.elapsed = 0f; 
            }
        }
        else
            this.elapsed = 0f;
    }
}
