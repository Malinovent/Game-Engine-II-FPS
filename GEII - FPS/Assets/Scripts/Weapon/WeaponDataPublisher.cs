public static class WeaponDataPublisher
{
    public static event System.Action<WeaponData> OnWeaponUpdated;

    public static void PublishData(WeaponAmmo ammo, string weaponName)
    {
        WeaponData data = new WeaponData(ammo.RemainingAmmo.ToString(), ammo.MaxAmmo.ToString(), ammo.RemainingMagazines.ToString(), weaponName);
        OnWeaponUpdated?.Invoke(data);
    }

    public static void PublishData(string remainingAmmo, string maxAmmo, string remainingMagazines, string weaponName)
    {
        WeaponData data = new WeaponData(remainingAmmo, maxAmmo, remainingMagazines, weaponName);
        OnWeaponUpdated?.Invoke(data);
    }
}