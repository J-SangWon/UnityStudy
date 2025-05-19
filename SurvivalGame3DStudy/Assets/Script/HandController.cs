using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Hand currentHand;
    bool isAttack;
    bool isSwing;

    RaycastHit hitInfo;

    void Start()
    {

    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackDelay());
            }

        }
    }

    IEnumerator AttackDelay()
    {
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");
        float currentAttackDelay = 0;

        yield return new WaitForSeconds(currentHand.attackDelayA);
        currentAttackDelay += currentHand.attackDelayA;
        isSwing = true;

        //공격 활성화 시점
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand.attackDelayB);
        currentAttackDelay += currentHand.attackDelayB;
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentAttackDelay);
        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log("Hit : " + hitInfo.transform.name);
                yield return null;
            }
            else
            {
                yield return null;
            }
        }

    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range))
            return true;
        else
            return false;
    }

}
