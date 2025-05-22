using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon;

    [SerializeField] float changeWeaponDelayTime;
    [SerializeField] float changeWeaponEndDelayTime;

    [SerializeField] Gun[] guns;
    [SerializeField] CloseWeapon[] hands;
    [SerializeField] CloseWeapon[] axes;
    [SerializeField] CloseWeapon[] pickaxe;

    Dictionary<string, Gun> gunDict = new Dictionary<string, Gun>();
    Dictionary<string, CloseWeapon> handDict = new Dictionary<string, CloseWeapon>();
    Dictionary<string, CloseWeapon> axeDict = new Dictionary<string, CloseWeapon>();
    Dictionary<string, CloseWeapon> pickaxeDict = new Dictionary<string, CloseWeapon>();

    [SerializeField] GunController gunController;
    [SerializeField] HandController handController;
    [SerializeField] AxeController axeController;
    [SerializeField] PickaxeController pickaxeController;

    [SerializeField] string currentWeaponType;
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDict.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDict.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDict.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxe.Length; i++)
        {
            pickaxeDict.Add(pickaxe[i].closeWeaponName, pickaxe[i]);
        }
    }

    private void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeWeaponCoroutine("HAND", "맨손"));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                StartCoroutine(ChangeWeaponCoroutine("AXE", "Axe"));
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                StartCoroutine(ChangeWeaponCoroutine("PICKAXE", "Pickaxe"));



        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CanclePreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);
        currentWeaponType = _type;
        isChangeWeapon = false;
    }
    void CanclePreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "GUN":
                gunController.CancleFineSight();
                gunController.CancelReload();
                GunController.isActivate = false;
                break;
            case "HAND":
                HandController.isActivate = false;
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController.isActivate = false;
                break;
        }

    }

    void WeaponChange(string _type, string _name)
    {
        switch (_type)
        {
            case "GUN":
                gunController.GunChange(gunDict[_name]);
                break;
            case "HAND":
                handController.CloseWeaponChange(handDict[_name]);
                break;
            case "AXE":
                axeController.CloseWeaponChange(axeDict[_name]);
                break;
            case "PICKAXE":
                pickaxeController.CloseWeaponChange(pickaxeDict[_name]);
                break;
        }
    }


}
