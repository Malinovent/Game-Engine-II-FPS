using UnityEngine;

public class SMG : WeaponBase, IFireReleased, IReloadable
{
    [SerializeField] private WeaponRaycaster raycaster;
    [SerializeField] private WeaponRateOfFire RoF;
    [SerializeField] private WeaponAmmo ammo;

    private bool isFiring = false;

    private void FireWeapon()
    {
        if(raycaster.TryGetTarget(out RaycastHit hit))
        {
            //Do damage
        }

        RoF.FireShot();
        ammo.FireShot();
    }

    public override void UpdateWeapon()
    {
        ammo.UpdateReload(Time.deltaTime);
        RoF.UpdateFire(Time.deltaTime);

        if(RoF.CanFire && ammo.HasAmmo() && isFiring && !ammo.IsReloading)
        {
            FireWeapon();
        }
    }

    public override void FirePressed()
    {
        isFiring = true;
    }

    public void FireReleased()
    {
        isFiring = false;
    }

    public void Reload()
    {
        ammo.StartReload();
        isFiring = false;
    }

    protected override void UpdateWeaponData()
    {
        WeaponDataPublisher.PublishData(ammo, weaponName);
    }
}
