using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;

    private WeaponBase currentWeapon;
    private int currentWeaponIndex = 0;

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

public class WeaponBase : MonoBehaviour
{
    public string weaponName;
}

public class Sniper : WeaponBase
{ 


}
