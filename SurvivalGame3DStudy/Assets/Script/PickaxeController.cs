using System.Collections;
using UnityEngine;

public class PickaxeController : CloseWeaponController
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
    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if(hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
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

}
