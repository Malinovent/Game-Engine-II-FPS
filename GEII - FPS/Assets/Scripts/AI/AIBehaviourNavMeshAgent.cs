using UnityEngine;
using UnityEngine.AI;

public class AIBehaviourNavMeshAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    private Transform currentTarget;

    public void UpdateMovement()
    {
        if (!currentTarget)
            return;

        navMeshAgent.SetDestination(currentTarget.position);
    }

    public void MoveTo(Transform transform)
    {
        currentTarget = transform;
        navMeshAgent.SetDestination(transform.position);
    }

    public void MoveTo(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    public void Stop()
    {
        navMeshAgent.isStopped = true;
    }

    public void Resume()
    {
        navMeshAgent.isStopped = false;
    }
}
