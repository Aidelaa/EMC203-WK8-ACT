using System;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        TryGetComponent<TextMeshProUGUI>(out this.healthText);
    }
    
    private void Start()
    {
        this.playerHealth.OnHealthChanged.AddListener(UpdateText);
        UpdateText(this.playerHealth.currentHealth);
    }

    private void UpdateText(float health)
    {
        this.healthText.text = $"Health: {health:0}/{this.playerHealth.maxHealth}";
    }
}
