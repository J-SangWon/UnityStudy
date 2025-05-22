using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject[] EnemyPool = new GameObject[20];
    private int poolSize = 20; // 풀 크기
    private int currentIndex = 0; // 현재 인덱스
    void Start()
    {
        // 적 풀 초기화
        EnemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            EnemyPool[i] = Instantiate(enemyPrefab); // 적 프리팹을 인스턴스화
            EnemyPool[i].SetActive(false); // 초기에는 비활성화
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnEnemy(); // 적 스폰 함수 호출
        }
    }

    private void SpawnEnemy()
    {
        // 풀에서 비활성화된 적을 찾아서 활성화
        for (int i = 0; i < poolSize; i++)
        {
            if (!EnemyPool[i].activeInHierarchy) // 비활성화된 적 찾기
            {
                EnemyPool[i].SetActive(true); // 적 활성화
                EnemyPool[i].transform.position = transform.position; // 스폰 위치 설정
                break; // 한 개만 스폰하고 종료
            }
        }
    }
}
