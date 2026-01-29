using Unity.VisualScripting;
using UnityEngine;

public class Sniper : WeaponBase, IReloadable
{
    [SerializeField] WeaponRateOfFire rateOfFire;
    [SerializeField] WeaponAmmo weaponAmmo;
    [SerializeField] WeaponRaycaster raycaster;

    public override void UpdateWeapon()
    {
        rateOfFire.UpdateFire(Time.deltaTime);
        weaponAmmo.UpdateReload(Time.deltaTime);
    }

    private void FireWeapon()
    {
        raycaster.FireShot();
        rateOfFire.FireShot();
        weaponAmmo.FireShot();
    }

    public override void FirePressed()
    {
        if (weaponAmmo.HasAmmo() && rateOfFire.CanFire && !weaponAmmo.IsReloading)
        {
            FireWeapon();
        }
    }

    public void Reload()
    {
        weaponAmmo.StartReload();
    }
}
