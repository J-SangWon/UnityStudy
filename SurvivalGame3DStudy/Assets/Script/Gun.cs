using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range; //사정거리
    public float accuracy;
    public float fireRate;
    public float reloadTime; //공격 딜레이
    public int damage;

    public int reloadBulletCount; // 총알 재장선 갯수
    public int currentBulletCount; // 남은 탄 수
    public int maxBulletCount; // 최대 소유가능 탄 갯수
    public int carryBulletCount; //현재 소유 탄 갯수

    public float retroActionForce; //반동
    public float retroActionFineSightForce; //정조준 반동 세기

    public Vector3 fineSigthOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;
}
