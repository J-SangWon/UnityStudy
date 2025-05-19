using System.Collections;
using UnityEngine;
public enum AttackState
{
    Idle,
    Windup,
    Impact,
    Cooldown
}
public class MeeleFighter : MonoBehaviour
{
    [SerializeField] GameObject sword;
    BoxCollider swordColider;

    Animator anim;
    public bool inAction { get; private set; } = false;
    AttackState attackState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (sword)
        {
            swordColider = sword.GetComponent<BoxCollider>();
            swordColider.enabled = false;

        }
    }

    public void TryToAttack()
    {
        if (!inAction)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator PlayHitReaction()
    {
        inAction = true;
        anim.CrossFade("Sword Impact", 0.2f);
        yield return null;

        var animState = anim.GetNextAnimatorStateInfo(1);
        yield return new WaitForSeconds(animState.length);

        inAction = false;
    }

    IEnumerator Attack()
    {
        inAction = true;

        attackState = AttackState.Windup;

        float impactStartTime = 0.33f;
        float impactEndTime = 0.55f;

        anim.CrossFade("Slash", 0.2f);
        yield return null;

        var animState = anim.GetNextAnimatorStateInfo(1);

        float timer = 0f;

        while (timer <= animState.length)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / animState.length;
            if (attackState == AttackState.Windup)
            {
                if (normalizedTime >= impactStartTime)
                {
                    attackState = AttackState.Impact;
                    //콜라이더 키고 끄기
                    swordColider.enabled = true;
                }
                
            }
            else if (attackState == AttackState.Impact)
            {
                if (normalizedTime >= impactEndTime)
                {
                    attackState = AttackState.Cooldown;
                    //콜라이더 끄기
                    swordColider.enabled = false;
                }
            }
            else if (attackState == AttackState.Cooldown)
            {

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
            StartCoroutine(PlayHitReaction());
        }
    }

}
