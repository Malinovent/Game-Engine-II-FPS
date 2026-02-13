using UnityEngine;

public class RocketLauncher : WeaponBase
{
    [SerializeField] private WeaponAmmo ammo;
    [SerializeField] private WeaponRateOfFire RoF;
    [SerializeField] private WeaponProjectileLauncher projectileLauncher;

    public override void FirePressed()
    {
        if (RoF.CanFire && ammo.HasAmmo())
        {
            ammo.FireShot();
            ammo.StartReload();
            projectileLauncher.Fire();
            RoF.FireShot();

            UpdateWeaponData();
        }
    }

    public override void UpdateWeapon()
    {
        RoF.UpdateFire(Time.deltaTime);
    }

    protected override void UpdateWeaponData()
    {
        WeaponDataPublisher.PublishData(ammo, weaponName);
    }
}
