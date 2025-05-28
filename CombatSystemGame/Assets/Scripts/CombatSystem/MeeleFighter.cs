using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum AttackState
{
    Idle,
    Windup,
    Impact,
    Cooldown
}

public class MeeleFighter : MonoBehaviour
{
    [SerializeField] private List<AttackData> attacks;
    [SerializeField] private GameObject sword;
    [SerializeField] private float rotationSpeed = 500;
    private BoxCollider swordColider;
    private SphereCollider leftHandCol, rightHandCol, leftFootCol, rightFootCol;

    public Animator anim;
    public bool inAction { get; private set; } = false;
    public AttackState attackState { get; private set; }

    public bool InCounter;

    private bool doCombo;
    private int comboCount = 0;

    public event Action OnGoHit;
    public event Action OnHitComplete;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (sword)
        {
            swordColider = sword.GetComponent<BoxCollider>();

            leftHandCol = anim.GetBoneTransform(HumanBodyBones.LeftHand).GetComponent<SphereCollider>();
            rightHandCol = anim.GetBoneTransform(HumanBodyBones.RightHand).GetComponent<SphereCollider>();
            leftFootCol = anim.GetBoneTransform(HumanBodyBones.LeftFoot).GetComponent<SphereCollider>();
            rightFootCol = anim.GetBoneTransform(HumanBodyBones.RightFoot).GetComponent<SphereCollider>();
            DisableCol();
        }
    }

    private void DisableCol()
    {
        if (sword)
            swordColider.enabled = false;
        if (leftHandCol)
            leftHandCol.enabled = false;
        if (rightHandCol)
            rightHandCol.enabled = false;
        if (leftFootCol)
            leftFootCol.enabled = false;
        if (rightFootCol)
            rightFootCol.enabled = false;
    }

    private void EnableHitBox(AttackData attack)
    {
        switch (attack.HitboxToUse)
        {
            case AttackHitBox.LeftHand:
                leftHandCol.enabled = true;
                break;

            case AttackHitBox.RightHand:
                rightHandCol.enabled = true;
                break;

            case AttackHitBox.LeftFoot:
                leftFootCol.enabled = true;
                break;

            case AttackHitBox.RightFoot:
                rightFootCol.enabled = true;
                break;

            case AttackHitBox.Sword:
                swordColider.enabled = true;
                break;

            default:
                break;
        }
    }

    public void TryToAttack()
    {
        if (!inAction)
        {
            StartCoroutine(Attack());
        }
        else if (attackState == AttackState.Impact || attackState == AttackState.Cooldown)
        {
            doCombo = true;
        }
    }

    private IEnumerator PlayHitReaction(Transform attacker)
    {
        inAction = true;

        var dispVec = attacker.position - transform.position;
        dispVec.y = 0f;
        transform.rotation = Quaternion.LookRotation(dispVec);

        OnGoHit?.Invoke();

        anim.CrossFade("Sword Impact", 0.2f);
        yield return null;


        var animState = anim.GetNextAnimatorStateInfo(1);
        yield return new WaitForSeconds(animState.length * 0.8f);

        OnHitComplete?.Invoke();
        inAction = false;
    }

    public IEnumerator PerformCounterAttack(EnemyController opponent)
    {
        inAction = true;

        InCounter = true;
        opponent.fighter.InCounter = true;

        var dispVec = opponent.transform.position - transform.position;
        dispVec.y = 0f;
        transform.rotation = Quaternion.LookRotation(dispVec);
        opponent.transform.rotation = Quaternion.LookRotation(-dispVec);

        anim.CrossFade("CounterAttack", 0.2f);
        opponent.anim.CrossFade("CounterAttackVictim", 0.2f);
        opponent.ChangeState(EnemyStates.Dead);

        yield return null;

        var animState = anim.GetNextAnimatorStateInfo(1);
        yield return new WaitForSeconds(animState.length * 0.8f);

        InCounter = false;
        opponent.fighter.InCounter = false;

        inAction = false;
    }

    private IEnumerator Attack(Vector3? attackDir = null)
    {
        inAction = true;
        attackState = AttackState.Windup;

        if(attackDir != null)
        {
          transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(attackDir.Value), rotationSpeed * Time.deltaTime);
        }

        anim.CrossFade(attacks[comboCount].animName, 0.2f);
        yield return null;

        var animState = anim.GetNextAnimatorStateInfo(1);

        float timer = 0f;

        while (timer <= animState.length)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / animState.length;
            if (attackState == AttackState.Windup)
            {
                if (InCounter) break;
                if (normalizedTime >= attacks[comboCount].impactStartTime)
                {
                    attackState = AttackState.Impact;
                    //콜라이더 키고 끄기
                    //swordColider.enabled = true;
                    EnableHitBox(attacks[comboCount]);
                }
            }
            else if (attackState == AttackState.Impact)
            {
                if (normalizedTime >= attacks[comboCount].impactEndTime)
                {
                    attackState = AttackState.Cooldown;
                    //콜라이더 끄기
                    swordColider.enabled = false;
                    DisableCol();
                }
            }
            else if (attackState == AttackState.Cooldown)
            {
                if (doCombo)
                {
                    doCombo = false;

                    comboCount = (comboCount + 1) % (attacks.Count);

                    StartCoroutine(Attack());
                    yield break;
                }
            }
            yield return null;
        }

        attackState = AttackState.Idle;

        inAction = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitbox") && !inAction)
        {
            StartCoroutine(PlayHitReaction(other.GetComponentInParent<MeeleFighter>().transform));
        }
    }

    public List<AttackData> GetAttackDatas => attacks;

    public bool IsCounterable => attackState == AttackState.Windup && comboCount == 0;
}