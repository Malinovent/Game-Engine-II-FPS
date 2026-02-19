using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private WeaponRaycaster raycaster;

    private IInteractable currentInteractable;

    public void UpdateInteractor()
    {
        if(raycaster.TryGetTarget(out RaycastHit hit))
        {
            bool hasTargetAlready = currentInteractable != null;
            currentInteractable = hit.collider.GetComponent<IInteractable>();   

            if(currentInteractable != null)
            {
                UIEvents.OnCrosshairUpdated?.Invoke(Color.green);
            }
            else if(hasTargetAlready)
            {
                UIEvents.OnCrosshairUpdated?.Invoke(Color.black);
            }
        } else if (currentInteractable != null)
        {
            currentInteractable = null;
            UIEvents.OnCrosshairUpdated?.Invoke(Color.black);
        }
    }

    public void Interact()
    {
        currentInteractable?.Interact();
    }
}
