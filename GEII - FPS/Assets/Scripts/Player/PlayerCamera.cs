using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;
    [SerializeField] private float sensitivity = 0.5f;

    private float currentPitch;


    public void Pitch(float inputDelta)
    {
        currentPitch -= inputDelta * sensitivity; // invert if needed
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        cam.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
    }
}
