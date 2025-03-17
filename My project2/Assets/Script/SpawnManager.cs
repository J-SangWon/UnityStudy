using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //적 가지고 오기
    public GameObject enemy;
    public GameObject Boss;
    public GameObject NamedEnemy;
    private bool isBossSpawned = false;
    public static SpawnManager instance;

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //적 생성 함수
    void SpawnEnemy()
    {
        if (!isBossSpawned)
        {
            float randomX = Random.Range(-2f, 2f); //X좌표 범위
                                                   //적 생성
            Instantiate(enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
        }

    }
    void SpawnNamed()
    {
        if (!isBossSpawned)
        {
            float randomX = Random.Range(-2f, 2f);
            Instantiate(NamedEnemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
        }
    }
    //보스 생성 및 조건
    void SpawnBoss()
    {
        if (GameManager.instance.EnemyCount >= 10 && !isBossSpawned)
        {
            Instantiate(Boss, new Vector3(0, 3.35f, 0), Quaternion.identity);
            GameManager.instance.EnemyCount = 0;
            isBossSpawned = true;
        }

    }
    //보스 사망
    public void BossDie()
    {
        isBossSpawned = false;
    }


    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(3f);
        InvokeRepeating("SpawnEnemy", 1, 1.5f);
        InvokeRepeating("SpawnNamed", 1, 5f);
    }

    void Start()
    {
        StartCoroutine(SpawnDelay());
    }
    void Update()
    {
        SpawnBoss();
    }
}
