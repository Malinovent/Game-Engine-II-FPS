using System.Net.Configuration;
using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;

    private WeaponBase currentWeapon;
    private int currentWeaponIndex = 0;

    private void Start()
    {
        SelectWeapon(weapons[currentWeaponIndex]);
    }

    public void UpdateWeapon()
    {
        currentWeapon.UpdateWeapon();
    }

    public void OnFireWeaponPressed()
    {
        IFirePressed weapon = currentWeapon as IFirePressed;
        weapon?.FirePressed();

        if (weapon == null)
        {
            Debug.Log($"Failed to fire [PRESSED]: {currentWeapon.name} ");
        }
    }

    public void OnFireWeaponReleased()
    {
        IFireReleased weapon = currentWeapon as IFireReleased;
        weapon?.FireReleased();

        if (weapon == null)
        {
            Debug.Log($"Failed to fire [RELEASED]: {currentWeapon.name} ");
        }
    }

    public void OnReload()
    {
        IReloadable reloadable = currentWeapon as IReloadable;
        reloadable?.Reload();

        if(reloadable == null)
        {
            Debug.Log($"Failed to load: {currentWeapon.name} ");
        }
        //currentWeapon.OnReload();
    }

    private void SelectWeapon(WeaponBase weapon)
    {
        currentWeapon = weapon;
    }

    public void SelectWeapon(int weaponIndex)
    {
        currentWeaponIndex = Mathf.Clamp(weaponIndex, 0, weapons.Length - 1);
        SelectWeapon(weapons[currentWeaponIndex]);
    }

    public void NextWeapon()
    {
        currentWeaponIndex++;
        if(currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }
    }

    public void PreviousWeapon()
    {
        currentWeaponIndex--;
        if(currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
    }
}

public interface IReloadable
{
    public void Reload();
}

public interface IFirePressed
{
    public void FirePressed();
}

public interface IFireReleased
{
    public void FireReleased();
}