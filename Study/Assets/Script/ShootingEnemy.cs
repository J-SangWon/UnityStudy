using System;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("적 캐릭터 속성")]
    public float detectionRange = 10f;
    public float shootingInterval = 2f;
    public GameObject missilePrefab;

    [Header("참조 컴포넌트")]
    public Transform firePoint;
    private Transform player;
    private float shootTimer;
    private SpriteRenderer SP;
    private Animator animator;
    void Start()
    {
        //컴포넌트 초기화
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SP = GetComponent<SpriteRenderer>();
        shootTimer = shootingInterval;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;     //플레이어가 없으면 실행하지 않음

        //플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            //플레이어 방향으로 스프라이트 회전
            SP.flipX = (player.position.x < transform.position.x);


            //미사일 발사 로직
            shootTimer -= Time.deltaTime;   //타이머 감소

            if (shootTimer <= 0)
            {
                Shoot();            //미사일 발사
                shootTimer = shootingInterval; //타이머 리셋
            }

        }
    }

    void Shoot()
    {
        //미사일 생성
        GameObject missile = Instantiate(missilePrefab, firePoint.position, Quaternion.identity);

        //플레이어 방향으로 발사 방향 설정
        Vector2 direction = (player.position - firePoint.position).normalized;
        missile.GetComponent<EnemyMissile>().SetDirection(direction);
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("Death", true);
        //애니메이션 종료 후 오브젝트 제거
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }


    //디버깅용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
