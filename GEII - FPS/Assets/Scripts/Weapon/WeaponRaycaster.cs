using UnityEngine;

public class WeaponRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask validLayers;
    [SerializeField] private float maxDistance = 200f;

    public Camera mainCamera { get; private set; }

    private RaycastHit currentTarget;
    private bool hasTarget;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public bool TryGetTarget(out RaycastHit hit)
    {
        hit = currentTarget;
        return hasTarget && currentTarget.collider != null;
    }

    public void UpdateTargetFromMouse()
    {
        if (!mainCamera)
        {
            hasTarget = false;
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, maxDistance, validLayers, QueryTriggerInteraction.Ignore))
        {
            currentTarget = hit;
            hasTarget = true;
        }
        else
        {
            currentTarget = default;
            hasTarget = false;
        }
    }

    public Vector3 GetAimPoint(Vector3 fallbackOrigin, Vector3 fallbackForward)
    {
        if (hasTarget && currentTarget.collider != null)
            return currentTarget.point;

        // If nothing hit, aim straight ahead into space (useful for shooting at nothing)
        return fallbackOrigin + fallbackForward * maxDistance;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || !mainCamera) return;

        Gizmos.color = hasTarget ? Color.red : Color.gray;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(ray.origin, ray.direction * maxDistance);

        UpdateTargetFromMouse();

        if (hasTarget)
            Gizmos.DrawSphere(currentTarget.point, 0.15f);
    }
}
