using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [Header("Reload Parameters")]
    [SerializeField] private int maxMagazine = 3;
    [SerializeField] private float reloadTime = 2f;

    private float reloadTimer = 0f;

    private bool isReloading = false;
    public bool IsReloading => isReloading;


    [Header("Ammo Parameters")]
    [SerializeField] private int maxAmmo = 10;


    private int currentAmmo = 10;
    private int currentMagazine = 3;


    private void Awake()
    {
        currentAmmo = maxAmmo;
        currentMagazine = maxMagazine;
    }

    public void UpdateReload(float deltaTime)
    {
        if (isReloading)
        {
            reloadTimer += deltaTime;
            if(reloadTimer >= reloadTime)
            {
                Reload();
            }
        } 
    }

    public void FireShot()
    {
        currentAmmo = Mathf.Max(0, currentAmmo - 1);
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    public bool CanReload()
    {
        return currentMagazine > 0;
    }

    public void StartReload()
    {
        if (!CanReload())
            return;

        isReloading = true;
    }

    private void Reload()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
        reloadTimer = 0f;
        currentMagazine = Mathf.Max(0, currentMagazine - 1);
        Debug.Log("Reloaded. Current Magazine: " + currentMagazine);
    }
}