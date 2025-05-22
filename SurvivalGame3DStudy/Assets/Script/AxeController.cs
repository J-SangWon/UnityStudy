using System.Collections;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    public static bool isActivate;

    void Start()
    {

    }

    void Update()
    {
        if (isActivate)
        {
            Attack();

        }

    }
    protected override IEnumerator HitCoroutine()
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
    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;

    }
}
