using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float currentHealth
    {
        get => this._currentHealth;
        set
        {
            this._currentHealth = Mathf.Clamp(value, 0, this.maxHealth);
            
            this.OnHealthChanged.Invoke(this._currentHealth);
        }
    }

    private float _currentHealth;
    public UnityEvent<float> OnHealthChanged;

    private void Awake()
    {
        this._currentHealth = this.maxHealth;
    }
}
