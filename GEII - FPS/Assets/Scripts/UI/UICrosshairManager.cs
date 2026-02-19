using UnityEngine;
using UnityEngine.UI;

public class UICrosshairManager : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;

    private void OnEnable()
    {
        UIEvents.OnCrosshairUpdated += SetCrosshairColor;
    }

    private void OnDisable()
    {
        UIEvents.OnCrosshairUpdated -= SetCrosshairColor;
    }


    private void SetCrosshairColor(Color color)
    {
        crosshairImage.color = color;
    }
}
