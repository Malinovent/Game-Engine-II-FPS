using UnityEngine;
using System;

public abstract class WeaponBase : MonoBehaviour, IFirePressed
{
    public string weaponName;

    private void OnEnable()
    {
        UpdateWeaponData();
    }

    public abstract void FirePressed();

    public abstract void UpdateWeapon();

    protected abstract void UpdateWeaponData();

}
