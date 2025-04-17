using UnityEngine;

public class Slash : MonoBehaviour
{
    private GameObject p;
    Vector2 MousePos;
    Vector2 dir;

    float angle;
    Vector3 dirNo;

    public Vector3 direction = Vector3.right;




    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");

        Transform tr = p.GetComponent<Transform>();
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);
        dir = Pos - tr.position;

        //바라보는 각도 구하기
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = p.transform.position;
    }

    public void Des()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적 미사일 충돌 확인
        if (collision.gameObject.GetComponent<EnemyMissile>() != null)
        {
            //미사일 현재 방향 가져오기
            EnemyMissile missile = collision.gameObject.GetComponent<EnemyMissile>();
            SpriteRenderer missileSprite = collision.gameObject.GetComponent<SpriteRenderer>();

            //반대 방향으로 날리기
            Vector2 ReverseDir = -missile.GetDirection();

            //미사일의 새로운 방향 설정
            missile.SetDirection(ReverseDir);

            //스프라이트 방향 뒤집기
            if (missileSprite != null)
            {
                missileSprite.flipX = !missileSprite.flipX;
            }
        }
        //적처리
        if (collision.CompareTag("Enemy"))
        {
            //적 죽음 애니메이션 실행
            ShootingEnemy enemy = collision.GetComponent<ShootingEnemy>();
            if(enemy != null)
            {
                enemy.PlayDeathAnimation();
            }
        }

    }


}
