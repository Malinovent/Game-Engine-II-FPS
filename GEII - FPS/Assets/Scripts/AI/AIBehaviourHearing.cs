using System;
using UnityEngine;

public class AIBehaviourHearing : MonoBehaviour
{
    [SerializeField] private float hearingRange = 10f;

    public event Action<Transform> onHeard;
    private bool isPlayerInRange = false;

    public void UpdateHearing()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, hearingRange);

        foreach(Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                SetIsPlayerInRange(true, col.transform);
                return;
            }
        }

        //If player is not found, then set the player in range to false, and send null as the target
        SetIsPlayerInRange(false, null);
    }

    private void SetIsPlayerInRange(bool value, Transform target)
    {
        if(!isPlayerInRange && value == true)
        {
            onHeard?.Invoke(target);
            Debug.Log("Heard player, invoking event");
        }

        isPlayerInRange = value;        
    }

    public void ResetBehaviour()
    {
        isPlayerInRange = false;        
    }


    private void OnDrawGizmos()
    {
        if(isPlayerInRange)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(this.transform.position, hearingRange);
    }
}
