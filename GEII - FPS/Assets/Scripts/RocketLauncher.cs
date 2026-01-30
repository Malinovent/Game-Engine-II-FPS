using UnityEngine;

public class RocketLauncher : WeaponBase
{
    [SerializeField] private WeaponRateOfFire RoF;
    [SerializeField] private WeaponProjectileLauncher projectileLauncher;

    public override void FirePressed()
    {
        if (RoF.CanFire)
        {
            projectileLauncher.Fire();
            RoF.FireShot();
        }
    }

    public override void UpdateWeapon()
    {
        RoF.UpdateFire(Time.deltaTime);
    }
}
