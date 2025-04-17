using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 5f;
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            SpawnRandomEnemy();
            _timer = 0;
        }
    }

    private void SpawnRandomEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-10f, 10f), 
            0, 
            Random.Range(-10f, 10f)
            );

        EnemyType type = (EnemyType)Random.Range(0, 3);
        IEnemy enemy = EnemyFactory.Instance.CreateEnemy(type, spawnPosition);
        Debug.Log($"{type} 적이 {spawnPosition}에 생성되었습니다");
    }

}
