using System.Transactions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public GameObject Explosion;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(0, MoveSpeed*Time.deltaTime, 0);
    }

    private void OnBecameInvisible()
    {
        //화면 밖으로 나가면 지워짐
        Destroy(gameObject);
    }

    //2D충돌 트리거 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //미사일과 적이 충돌
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //폭발 이펙트 생성
            Instantiate(Explosion, transform.position, Quaternion.identity);
            //적 사망 사운드
            SoundManager.instance.SoundDie();
            //점수
            GameManager.instance.AddScore(10);
            //적 지우기
            Destroy(collision.gameObject);
            //총알 지우기
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("NamedEnemy"))
        {
            //폭발 이펙트 생성
            Instantiate(Explosion, transform.position, Quaternion.identity);
            NamedEnemy named = collision.gameObject.GetComponent<NamedEnemy>();
            named.NamedHP--;
            if(named.NamedHP <= 0)
            {
                SoundManager.instance.SoundDie();
                GameManager.instance.AddScore(30);
                Destroy(collision.gameObject);

            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            Instantiate(Explosion, transform.position,Quaternion.identity);
            Boss boss = collision.gameObject.GetComponent<Boss>();
            boss.BossHP--;
            if(boss.BossHP <= 0)
            {
                // 보스 제거
                Destroy(collision.gameObject);
                // 점수 추가
                GameManager.instance.AddScore(100);
                boss.BossHP = 10;
            }
            Destroy(gameObject);


        }
    }

    //충돌조건
    //1.각각의 오브젝트에 콜라이더 필요
    //2.두 오브젝트 중 하나에는 rigidbody가 필요함

    //OnCollisionEnter2D(Collosion2D collision) : trigger 체크안된 상태의 충돌
    //OnTriggerEnter2D(Collosion2D collision) : trigger(통과) 체크 된 상태에서의 충돌
    //stay : 머무를 때 , exit : 지나갈때


}
