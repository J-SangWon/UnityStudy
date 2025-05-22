using System.Collections;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    [SerializeField] protected CloseWeapon currentCloseWeapon;
    protected bool isAttack;
    protected bool isSwing;

    protected RaycastHit hitInfo;

    protected void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackDelay());
            }

        }
    }

    protected IEnumerator AttackDelay()
    {
        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");
        float currentAttackDelay = 0;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);
        currentAttackDelay += currentCloseWeapon.attackDelayA;
        isSwing = true;

        //공격 활성화 시점
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        currentAttackDelay += currentCloseWeapon.attackDelayB;
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentAttackDelay);
        isAttack = false;
    }

    protected abstract IEnumerator HitCoroutine();
    //{
    //    while (isSwing)
    //    {
    //        if (CheckObject())
    //        {
    //            isSwing = false;
    //            Debug.Log("Hit : " + hitInfo.transform.name);
    //            yield return null;
    //        }
    //        else
    //        {
    //            yield return null;
    //        }
    //    }

    //}

    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
            return true;
        else
            return false;
    }

    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;

        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
    }
}
