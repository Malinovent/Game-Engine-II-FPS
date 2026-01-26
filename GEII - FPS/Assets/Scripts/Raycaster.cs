using UnityEngine;

public class Raycaster : MonoBehaviour
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
        if(Physics.Raycast(ray, out hit, distance, validLayers))
        {
            return hit;
        }

        return hit;
    }

    public Vector3 GetMouseWorldPosition()
    {
        if (!mainCamera)
            return this.transform.position;

        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

}