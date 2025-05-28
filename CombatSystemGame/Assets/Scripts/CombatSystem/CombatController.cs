using UnityEngine;

public class CombatController : MonoBehaviour
{
    public EnemyController targetEnemy;

    public EnemyController TargetEnemy
    {
        get => targetEnemy;
        set
        {
            targetEnemy = value;

            if (targetEnemy == null)
                CombatMode = false;
        }
    }

    [SerializeField] private MeeleFighter meeleFighter;
    private Animator anim;
    private CameraController cam;

    private bool combatMode;

    public bool CombatMode
    {
        get => combatMode;
        set
        {
            combatMode = value;

            if (TargetEnemy == null)
                combatMode = false;

            anim.SetBool("combatMode", combatMode);
        }
    }

    private void Awake()
    {
        meeleFighter = GetComponent<MeeleFighter>();
        anim = GetComponent<Animator>();
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var enemy = EnemyManager.instance.GetAttackingEnemy();
            if (enemy && enemy.fighter.IsCounterable && !meeleFighter.inAction)
            {
                StartCoroutine(meeleFighter.PerformCounterAttack(enemy));
            }
            else
            {
                meeleFighter.TryToAttack();
                CombatMode = true;
            }
        }

        if (Input.GetButtonDown("LockOn"))
        {
            CombatMode = !CombatMode;
        }
    }

    private void OnAnimatorMove()
    {
        if (!meeleFighter.InCounter)
        {
            transform.position += anim.deltaPosition;
        }

        transform.rotation *= anim.deltaRotation;
    }

    public Vector3 GetTargetingDir()
    {
        var vecFromCam = transform.position - cam.transform.position;
        vecFromCam.y = 0f;

        return vecFromCam.normalized;
    }
}