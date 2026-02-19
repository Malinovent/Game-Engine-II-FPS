using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] DamageInfo damageInfo;

    public void Damage(IDamageable target)
    {
        target?.TakeDamage(damageInfo);
    }

    public void Damage(Collider target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        Damage(damageable);
        
    }
}
