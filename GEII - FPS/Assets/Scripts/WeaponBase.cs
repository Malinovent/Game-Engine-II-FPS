using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;

    public abstract void UpdateWeapon();

    public abstract void OnFirePressed();
    public abstract void OnFireReleased();
    public abstract void OnReload();
}
