using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private UnityEvent onDamageTaken;

    private int currentHealth = 0;
    public bool IsDead => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (IsDead)
            return;

        onDamageTaken?.Invoke();
        currentHealth -= info.amount;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

}