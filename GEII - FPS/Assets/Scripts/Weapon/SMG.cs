using UnityEngine;

public class SMG : WeaponBase, IFireReleased, IReloadable
{
    [SerializeField] private WeaponRaycaster raycaster;
    [SerializeField] private WeaponRateOfFire RoF;
    [SerializeField] private WeaponAmmo ammo;
    [SerializeField] private Damager damager;

    private bool isFiring = false;
    private IDamageable currentTarget;

    void Start()
    {
        ammo.OnReloadFinished += UpdateWeaponData;
    }

    private void FireWeapon()
    {
        damager.Damage(currentTarget);

        RoF.FireShot();
        ammo.FireShot();
        UpdateWeaponData();
    }

    public override void UpdateWeapon()
    {
        ammo.UpdateReload(Time.deltaTime);
        RoF.UpdateFire(Time.deltaTime);

        if (raycaster.TryGetTarget(out RaycastHit hit))
        {
            bool hasTargetAlready = currentTarget != null;
            currentTarget = hit.collider.GetComponent<IDamageable>();

            if(currentTarget == null && hasTargetAlready)
            {
                UIEvents.OnCrosshairUpdated?.Invoke(Color.black);
            }
            else if(currentTarget != null)
            {
                UIEvents.OnCrosshairUpdated?.Invoke(Color.red);
            }
        } else if (currentTarget != null)
        {
            currentTarget = null;
            UIEvents.OnCrosshairUpdated?.Invoke(Color.black);
        }

        if (RoF.CanFire && ammo.HasAmmo() && isFiring && !ammo.IsReloading)
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
