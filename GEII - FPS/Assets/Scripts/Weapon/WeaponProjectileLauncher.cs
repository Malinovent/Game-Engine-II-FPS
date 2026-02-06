using UnityEngine;

public class WeaponProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private WeaponRaycaster weaponRaycaster;

    public void Fire()
    {
        weaponRaycaster.UpdateTargetFromMouse();

        Vector3 aimPoint = weaponRaycaster.GetAimPoint(firePoint.position, firePoint.forward);
        Vector3 direction = (aimPoint - firePoint.position).normalized;

        Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || firePoint == null || weaponRaycaster == null) return;

        Vector3 aimPoint = weaponRaycaster.GetAimPoint(firePoint.position, firePoint.forward);
        Vector3 direction = (aimPoint - firePoint.position).normalized;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(firePoint.position, direction * 10f);
        Gizmos.DrawSphere(aimPoint, 0.15f);
    }

}
