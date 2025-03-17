using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet;
    void Start()
    {
        //(함수이름, 초기지연시간, 지연할 시간)  반복해서 함수를 실행하는 함수
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }
    void Shoot()
    {
        //미사일 프리팹, 런쳐포지션, 방향값 안줌
        Instantiate(bullet, transform.position, Quaternion.identity);

        //사운드
        SoundManager.instance.PlayBulletSound();

    }
    void Update()
    {
        
    }
}
