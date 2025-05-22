using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName; //너클이나 맨손 구분

    // 웨폰 유형
    public bool isHand;
    public bool isAxe;
    public bool isPickaxe;

    public float range; //공격범위
    public int damage;
    public float workSpeed;
    public float attackDelay; //공격 딜레이
    public float attackDelayA; //공격 활성화 시점
    public float attackDelayB; //공격 비활성화 시점

    public Animator anim;


}
