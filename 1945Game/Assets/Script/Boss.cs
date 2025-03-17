using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int flag = 1;
    int speed = 2;

    public GameObject ms1;
    public GameObject ms2;
    public Transform pos1;
    public Transform pos2;
    void Start()
    {
        StartCoroutine(BossMissle());
        StartCoroutine(CircleFire());
    }

    void Update()
    {
        if (transform.position.x >= 1) flag *= -1;
        if (transform.position.x <= -1) flag *= -1;

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
    }

    IEnumerator BossMissle()
    {
        while (true)
        {
            //미사일 두개
            Instantiate(ms1, pos1.position, Quaternion.identity);
            Instantiate(ms1, pos2.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }

    //원 방향으로 미사일 발사
    IEnumerator CircleFire()
    {
        //공격주기
        float attackRate = 3;
        //발사체 생성 갯수
        int count = 30;
        //발사체 사이 각도
        float intervalAnle = 360 / count;
        //가중되는 각도
        float weightAngle = 0f; ;
        //생성
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(ms2, transform.position, Quaternion.identity);

                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAnle * i;
                //발사체 이동 방향(벡터)
                //Cos
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //Sin 
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //발사체 이동 방향 설정
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

            }
            weightAngle++;

            yield return new WaitForSeconds(attackRate);

        }


    }
}