using System;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float distanceThreshold = 0.5f;
    [SerializeField] private float waitTimeAtWaypoint = 2f;

    private Transform currentWaypoint;
    private int currentWaypointIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    public void StartWaypointBehaviour(bool fromBeginning)
    {
        if (waypoints.Length == 0) return;

        if (fromBeginning)
        {
            currentWaypoint = waypoints[0];
        }
        else
        {
            currentWaypoint = waypoints[currentWaypointIndex];
        }

        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.SetDestination(currentWaypoint.position);
        Debug.Log("Starting patrol behaviour, moving to waypoint " + currentWaypointIndex);
    }

    private void NextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0; // Loop back to the first waypoint
        }

        currentWaypoint = waypoints[currentWaypointIndex];
        navMeshAgent.SetDestination(currentWaypoint.position);

        Debug.Log("Moving to next waypoint: " + currentWaypointIndex);
    }


    public void UpdateBeahviour()
    {
        if(isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > waitTimeAtWaypoint)
            {
                NextWaypoint();
                navMeshAgent.isStopped = false;
                isWaiting = false;
            }
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, currentWaypoint.position);
        if (distanceToTarget < distanceThreshold)
        {
            Debug.Log("Moving towards waypoint " + currentWaypointIndex + ", distance: " + distanceToTarget);
            navMeshAgent.isStopped = true;
            isWaiting = true;
            waitTimer = 0;
        }
    }

    //Draw a point to each waypoint, add a number to each point to show the order of the waypoints, and
    //draw a line between each waypoint to show the path the AI will take.
    //Use a different color for the current waypoint. Show the distance threshold
    private void OnDrawGizmos()
    {
        if (waypoints.Length == 0) return;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = (i == currentWaypointIndex) ? Color.red : Color.green;
            Gizmos.DrawSphere(waypoints[i].position, 0.3f);
            Gizmos.color = Color.blue;
            if (i < waypoints.Length - 1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }

        // Draw distance threshold around the current waypoint
        if (currentWaypoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(currentWaypoint.position, distanceThreshold);
        }

    }
}
