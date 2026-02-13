using UnityEngine;

public class RocketLauncher : WeaponBase
{
    [SerializeField] private WeaponAmmo ammo;
    [SerializeField] private WeaponRateOfFire RoF;
    [SerializeField] private WeaponProjectileLauncher projectileLauncher;

    private void Start()
    {
        ammo.OnReloadFinished += UpdateWeaponData;
    }

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
        ammo.UpdateReload(Time.deltaTime);
    }

    protected override void UpdateWeaponData()
    {
        WeaponDataPublisher.PublishData(ammo.RemainingAmmo.ToString(), ammo.RemainingMagazines.ToString(), "", weaponName);
    }
}
