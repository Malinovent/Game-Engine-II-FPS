using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IFirePressed
{
    public string weaponName;

    public abstract void FirePressed();

    public abstract void UpdateWeapon();

}
