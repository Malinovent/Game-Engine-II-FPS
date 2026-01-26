using Unity.VisualScripting;
using UnityEngine;

public class Sniper : WeaponBase
{
    [SerializeField] Raycaster raycaster;
    [SerializeField] WeaponRateOfFire rateOfFire;
    [SerializeField] WeaponAmmo weaponAmmo;

    public override void UpdateWeapon()
    {
        rateOfFire.UpdateFire(Time.deltaTime);
        weaponAmmo.UpdateReload(Time.deltaTime);
    }

    private void FireWeapon()
    {
        Vector3 startingPosition = raycaster.GetMouseWorldPosition();
        Ray ray = new Ray(startingPosition, transform.forward);
        RaycastHit hit = raycaster.GetRaycastTarget(ray, 100f);
        rateOfFire.FireShot();
        weaponAmmo.FireShot();

        if (hit.collider != null)
        {
            Debug.Log("Sniper hit: " + hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startingPosition = raycaster.GetMouseWorldPosition();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startingPosition, transform.forward * 100f);
    }

    public override void OnFirePressed()
    {
        if (weaponAmmo.HasAmmo() && rateOfFire.CanFire && !weaponAmmo.IsReloading)
        {
            FireWeapon();
        }
    }

    public override void OnFireReleased()
    {
        
    }

    public override void OnReload()
    {
        weaponAmmo.StartReload();
    }
}
