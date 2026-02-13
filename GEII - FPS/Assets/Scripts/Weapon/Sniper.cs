using Unity.VisualScripting;
using UnityEngine;

public class Sniper : WeaponBase, IReloadable
{
    [SerializeField] WeaponRateOfFire rateOfFire;
    [SerializeField] WeaponAmmo weaponAmmo;
    [SerializeField] WeaponRaycaster raycaster;

    void Start()
    {
        weaponAmmo.OnReloadFinished += UpdateWeaponData;
    }

    public override void UpdateWeapon()
    {
        rateOfFire.UpdateFire(Time.deltaTime);
        weaponAmmo.UpdateReload(Time.deltaTime);
    }

    private void FireWeapon()
    {
        if (raycaster.TryGetTarget(out RaycastHit hit))
        {
            //Do damage
        }

        rateOfFire.FireShot();
        weaponAmmo.FireShot();

        UpdateWeaponData();
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

    protected override void UpdateWeaponData()
    {
        WeaponDataPublisher.PublishData(weaponAmmo, weaponName);
    }
}
