using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = default;
    public int CurrentHealth { get; private set; }

    [Header("Damage Settings")]
    [SerializeField] private float invincibilityDuration = default;
    private bool isInvincible = false;

    //components
    private PlayerStateMachine stateMachine;

    //events
    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible || CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        //if health go under 0 then die else step into invincibleState
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {

        }
    }

    public void StartInvicible()
    {
        
    }

    public void EndInvicible()
    {
        
    }

    public void Die()
    {
        stateMachine.SwitchState(stateMachine.DeathState);
    }
}