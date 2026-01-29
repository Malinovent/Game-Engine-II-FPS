using UnityEngine;

public class WeaponRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask validLayers;
    [HideInInspector] public Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public RaycastHit GetRaycastTarget(Ray ray, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, validLayers))
        {
            return hit;
        }

        return hit;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (!mainCamera)
            return this.transform.position;

        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void FireShot()
    {
        Vector3 startingPosition = GetMouseWorldPosition();
        Ray ray = new Ray(startingPosition, transform.forward);
        RaycastHit hit = GetRaycastTarget(ray, 100f);        

        if (hit.collider != null)
        {
            Debug.Log("hit: " + hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startingPosition = GetMouseWorldPosition();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startingPosition, transform.forward * 100f);
    }
}
