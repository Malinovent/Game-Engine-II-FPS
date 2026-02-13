using UnityEngine;

public class Knife : WeaponBase
{
    [SerializeField] private WeaponRateOfFire RoF;


    private void FireMelee()
    {
        RoF.FireShot();
    }

    public override void FirePressed()
    {
        if(RoF.CanFire)
        {
            FireMelee();
        }
    }

    public override void UpdateWeapon()
    {
        RoF.UpdateFire(Time.deltaTime);
    }

    protected override void UpdateWeaponData()
    {
        WeaponDataPublisher.PublishData("∞", "", "", weaponName);
    }
}
