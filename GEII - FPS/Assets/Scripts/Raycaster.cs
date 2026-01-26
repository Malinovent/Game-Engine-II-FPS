using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask validLayers;
    [SerializeField] private bool enableGizmos = true;

    public GameObject GetRaycastTarget(Ray ray, float distance)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distance, validLayers))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        if(enableGizmos)
        {
            
        }
    }
}