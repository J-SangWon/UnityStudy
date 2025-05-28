using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { Idle, CombatMovement, Attack, RetreatAfterAttack, GettingHit ,Dead }

public class EnemyController : MonoBehaviour
{
    [field: SerializeField] public float Fov { get; private set; } = 180f;
    public MeeleFighter Target { get; set; }
    public float combatMovementTimer { get; set; } = 0f;
    [field: SerializeField] public List<MeeleFighter> TargetsInRange { get; private set; } = new List<MeeleFighter>();
    public StateMachine<EnemyController> StateMachine { get; private set; }
    Dictionary<EnemyStates, State<EnemyController>> stateDict;
    public NavMeshAgent NavAgent { get; private set; }
    public Animator anim {  get; private set; }
    public MeeleFighter fighter { get; private set; }
    public VisionSensor visionSensor { get; set; }
    public CharacterController characterController;
    public SkinnedMeshHighlighter MeshHighlighter;

    Vector3 prevPos;

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fighter = GetComponent<MeeleFighter>();
        MeshHighlighter = GetComponent<SkinnedMeshHighlighter>();
        characterController = GetComponent<CharacterController>();

        stateDict = new Dictionary<EnemyStates, State<EnemyController>>();
        stateDict[EnemyStates.Idle] = GetComponent<IdleState>();
        stateDict[EnemyStates.CombatMovement] = GetComponent<CombatMovementState>();
        stateDict[EnemyStates.Attack] = GetComponent<EnemyAttackState>();
        stateDict[EnemyStates.RetreatAfterAttack] = GetComponent<RetreatAfterAttackState>();
        stateDict[EnemyStates.GettingHit] = GetComponent<GettingHitState>();
        stateDict[EnemyStates.Dead] = GetComponent<DeadState>();


        StateMachine = new StateMachine<EnemyController>(this);
        StateMachine.ChangeState(stateDict[EnemyStates.Idle]);

        fighter.OnGoHit += ReactToHit;

    }
    public void ChangeState(EnemyStates state)
    {
        StateMachine.ChangeState(stateDict[state]);
    }
    public bool IsInState(EnemyStates state)
    {
        return StateMachine.currentState == stateDict[state];
    }

    void ReactToHit()
    {
        ChangeState(EnemyStates.GettingHit);
    }

    private void Update()
    {
        StateMachine.Execute();

        var deltaPos = transform.position - prevPos;
        var velocity = deltaPos / Time.deltaTime;

        float forwardSpeed = Vector3.Dot(velocity, transform.forward);

        anim.SetFloat("forwardSpeed", forwardSpeed / NavAgent.speed, 0.2f, Time.deltaTime);
        float angle = Vector3.SignedAngle(transform.forward, velocity, Vector3.up);
        float strafeSpeed = Mathf.Sin(angle * Mathf.Deg2Rad);
        anim.SetFloat("strafeSpeed", strafeSpeed, 0.2f, Time.deltaTime);



        prevPos = transform.position;
    }

    public MeeleFighter FindTarget()
    {
        foreach (var target in TargetsInRange)
        {
            var vecToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(transform.position, vecToTarget);

            if (angle <= Fov / 2)
            {
                return target;
            }
        }
        return null;
    }


}
