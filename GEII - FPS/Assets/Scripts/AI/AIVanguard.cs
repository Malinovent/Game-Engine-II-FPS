using UnityEngine;
public class AIVanguard : AIEnemyBase
{
    [SerializeField] AIBehaviourNavMeshAgent behaviourNavMeshAgent;
    [SerializeField] AIBehaviourHearing behaviourHearing;

    [Header("Attack Settings")]
    [SerializeField] AIProjectileAttackBehaviour projectileAttack;
    [SerializeField] private float attackEnterRange = 5f;
    [SerializeField] private float attackExitRange = 7f;

    private Transform target;


    private VanguardStates currentState;

    private void Awake()
    {
        //Set initial idle state and subscribe to hearing event
        currentState = VanguardStates.Idle;
        behaviourHearing.onHeard += OnHeardPlayer;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
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
