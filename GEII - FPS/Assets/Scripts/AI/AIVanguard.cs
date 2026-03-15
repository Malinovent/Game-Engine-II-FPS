using UnityEngine;
using UnityEngine.AI;
public class AIVanguard : AIEnemyBase
{
    [Header("References")]
    [SerializeField] AIBehaviourNavMeshAgent behaviourNavMeshAgent;
    [SerializeField] AIBehaviourHearing behaviourHearing;
    [SerializeField] AIPatrolBehaviour patrolBehaviour;
    [SerializeField] Animator animator;

    [Header("Attack Settings")]
    [SerializeField] AIProjectileAttackBehaviour projectileAttack;
    [SerializeField] private float attackEnterRange = 5f;
    [SerializeField] private float attackExitRange = 7f;

    private Quaternion previousRotation;

    private Transform target;


    private VanguardStates currentState;

    private void Awake()
    {
        //Set initial idle state and subscribe to hearing event
        currentState = VanguardStates.Idle;
        patrolBehaviour.StartWaypointBehaviour(true);
        behaviourHearing.onHeard += OnHeardPlayer;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        UpdateLocomotionAnimations();

        switch (currentState)
        {
            case VanguardStates.Idle:
                IdleBehaviour();
                break;
            case VanguardStates.MovingToTarget:
                MoveToBehaviour();
                break;
            case VanguardStates.Attacking:
                AttackBehaviour();
                break;
        }
    }


    private void SetState(VanguardStates newState)
    {
        if (currentState == newState || currentState == VanguardStates.Die) return;
        currentState = newState;

        switch (currentState)
        {
            case VanguardStates.Idle:
                behaviourNavMeshAgent.Stop();
                behaviourHearing.onHeard += OnHeardPlayer;
                behaviourHearing.ResetBehaviour();
                patrolBehaviour.StartWaypointBehaviour(false);
                break;
            case VanguardStates.MovingToTarget:
                behaviourNavMeshAgent.Resume();
                behaviourNavMeshAgent.MoveTo(target);
                break;
            case VanguardStates.Attacking:
                behaviourNavMeshAgent.Stop();
                break;
        }
    }

    private void UpdateLocomotionAnimations()
    {
        NavMeshAgent navMeshAgent = behaviourNavMeshAgent.GetNavMeshAgent();

        // Forward speed: normalize against agent's maxSpeed (0 to 1)
        float currentSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;        
        // Turn velocity: dot product of right vector against movement direction (-1 to 1)
        float currentTurn = 0f;
        if (navMeshAgent.velocity.sqrMagnitude > 0.01f)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity.normalized);

            float angleDelta = Vector3.SignedAngle(
            previousRotation * Vector3.forward,  // where we were facing last frame
            transform.forward,                   // where we're facing now
            Vector3.up                           // measured around the Y axis
        );

            // Normalize to -1..1 based on your rotationSpeed
            currentTurn = Mathf.Clamp(angleDelta / (navMeshAgent.angularSpeed * Time.deltaTime), -1f, 1f);
        }

        previousRotation = transform.rotation;
        animator.SetFloat("forwardVelocity", currentSpeed);
        animator.SetFloat("turnVelocity", currentTurn);
    }

    private void OnHeardPlayer(Transform target)
    {
        Debug.Log("Heard player, moving to target");
        this.target = target;
        behaviourHearing.onHeard -= OnHeardPlayer;
        projectileAttack.SetTarget(target);
        SetState(VanguardStates.MovingToTarget);
    }

    private void IdleBehaviour()
    {
        behaviourHearing.UpdateHearing();
        patrolBehaviour.UpdateBeahviour();
    }

    private void MoveToBehaviour()
    {
        behaviourNavMeshAgent.UpdateMovement();

        if(Vector3.Distance(this.transform.position, target.position) <= attackEnterRange)
        {
            SetState(VanguardStates.Attacking);
        }
    }

    private void AttackBehaviour()
    {
        projectileAttack.UpdateBehaviour();
        behaviourNavMeshAgent.UpdateFacing();
        if (Vector3.Distance(this.transform.position, target.position) >= attackExitRange)
        {
            SetState(VanguardStates.MovingToTarget);
        }
    }
    
    private enum VanguardStates
    {
        Idle,
        MovingToTarget,
        Attacking,
        Die
    }
}
