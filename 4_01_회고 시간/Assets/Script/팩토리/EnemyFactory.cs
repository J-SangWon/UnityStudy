using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance;
    public static EnemyFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("EnemyFactory");
                _instance = go.AddComponent<EnemyFactory>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject Grunt;
    public GameObject Runner;
    public GameObject Tank;

    public IEnemy CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject enemyObject = null;

        // 적 타입에 따라 다른 프리팹 사용
        switch (type)
        {
            case EnemyType.Grunt:
                enemyObject = Instantiate(Grunt);
                break;
            case EnemyType.Runner:
                enemyObject = Instantiate(Runner);
                break;
            case EnemyType.Tank:
                enemyObject = Instantiate(Tank);
                break;
            default:
                Debug.LogError($"Unknown enemy type: {type}");
                return null;
        }

        // 생성된 적 초기화
        IEnemy enemy = enemyObject.GetComponent<IEnemy>();
        enemy.Initialize(position);
        return enemy;
    }
}
