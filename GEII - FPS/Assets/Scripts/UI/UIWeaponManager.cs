using UnityEngine;
using TMPro;

public class UIWeaponManager : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText1;
    [SerializeField] TMP_Text ammoText2;
    [SerializeField] TMP_Text magazineText;
    [SerializeField] TMP_Text weaponName;

    private void OnEnable()
    {
        WeaponDataPublisher.OnWeaponUpdated += SetWeaponInfo;
    }

    public void OnDisable()
    {
        WeaponDataPublisher.OnWeaponUpdated -= SetWeaponInfo;
    }


    public void SetWeaponInfo(WeaponData weaponData)
    {
        ammoText1.SetText(weaponData.ammoText1);
        ammoText2.SetText("____\n" + weaponData.ammoText2);
        magazineText.SetText(weaponData.magazineText);
        weaponName.SetText(weaponData.weaponName);
    }
}
