using UnityEngine;

public class AIProjectileAttackBehaviour : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] WeaponRateOfFire weaponRateOfFire;
    [SerializeField] float aimVarianceAngle = 5f;
    [SerializeField] Transform firePoint;

    private Transform targetTransform;

    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }

    private Quaternion GetAimRotation()
    {
        Vector3 directionToPlayer = (targetTransform.position - firePoint.position).normalized;
        Quaternion aimRotation = Quaternion.LookRotation(directionToPlayer);
        float randomVariance = Random.Range(-aimVarianceAngle, aimVarianceAngle);
        return aimRotation * Quaternion.Euler(0, randomVariance, 0);
    }

    public void UpdateBehaviour()
    {
        if(targetTransform == null) 
            return;

        weaponRateOfFire.UpdateFire(Time.deltaTime);
        if (weaponRateOfFire.CanFire)
        {            
            Instantiate(projectilePrefab, firePoint.position, GetAimRotation());
            weaponRateOfFire.FireShot();
        }
    }
}
