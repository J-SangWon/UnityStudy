using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public GameObject Enemy;
    public GameObject Enemy2;


    public float SpawnStart = 1f;
    public float SpawnStop = 10f;
    bool swi = true;
    bool swi2 = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine("RandomSpawn");
        //InvokeRepeating("MonsterSpawn", 1, 1.5f);

        Invoke("stop", SpawnStop);
    }

    void Update()
    {
    }

    public void MonsterSpawn()
    {
        float randomX = Random.Range(-2f, 2f);
        Instantiate(Enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }

    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            yield return new WaitForSeconds(SpawnStart);
            float randomX = Random.Range(-2f, 2f);
            Instantiate(Enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
        }
    }
    IEnumerator RandomSpawn2()
    {

        while (swi2)
        {
            yield return new WaitForSeconds(SpawnStart);
            float randomX = Random.Range(-2f, 2f);
            Instantiate(Enemy2, new Vector3(randomX, transform.position.y), Quaternion.identity);
        }

    }

    void stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");
        StartCoroutine("RandomSpawn2");
    }
    void stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpan2");
    }

}
