using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
