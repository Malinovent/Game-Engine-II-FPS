[System.Serializable]
public struct WeaponData
{
    public string ammoText1;
    public string ammoText2;
    public string magazineText;
    public string weaponName;

    public WeaponData(string ammo1, string ammo2, string magazineText, string weaponName)
    {
        ammoText1 = ammo1;
        ammoText2 = ammo2;
        this.magazineText = magazineText;
        this.weaponName = weaponName;
    }
}