using UnityEngine;
using UnityEngine.AI;

public class AIBehaviourNavMeshAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform currentTarget;

    public void UpdateMovement()
    {
        if (!currentTarget)
            return;

        navMeshAgent.SetDestination(currentTarget.position);
    }

    public void UpdateFacing() // new — call this from AttackBehaviour()
    {
        if (!currentTarget) return;

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        direction.y = 0f; // stay upright

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }
}
