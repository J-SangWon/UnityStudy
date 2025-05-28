using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Bullet")]
    [SerializeField] Transform bulletPoint;
    [SerializeField] GameObject bulletObject;
    [SerializeField] float maxShootDelay = 0.2f;
    [SerializeField] float currentShootDelay;
    int maxBullet = 30;
    public int currentBullet = 0;
    [SerializeField] Text bulletText;

    [Header("Weapon FX")]
    [SerializeField] GameObject weaponFlashFX;
    [SerializeField] Transform bulletCasePoint;
    [SerializeField] GameObject bulletCaseFX;
    [SerializeField] Transform weaponClipPoint;
    [SerializeField] GameObject weaponClipFX;

    [Header("Enemy")]
    //[SerializeField] GameObject enemy;
    [SerializeField] GameObject[] spawnPoint;


    void Start()
    {
        instance = this;

        currentShootDelay = 0f;
        initBullet();

        StartCoroutine(EnemySpawn());

    }

    void Update()
    {
        bulletText.text = currentBullet + " / " + maxBullet;
    }

    public void Shooting(Vector3 targetPosition, Enemy enemy)
    {
        currentShootDelay += Time.deltaTime;
        if (currentShootDelay < maxShootDelay || currentBullet <= 0) return;
        currentBullet -= 1;
        currentShootDelay = 0f;
        Vector3 aim = (targetPosition - bulletPoint.position).normalized;

        //Instantiate(weaponFlashFX, bulletPoint);
        GameObject flashFx = PoolManager.instance.ActivateObject(1);
        SetObjectPosition(flashFx, bulletPoint);
        flashFx.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);
        //Instantiate(bulletCaseFX, bulletCasePoint);
        GameObject bulletCase = PoolManager.instance.ActivateObject(2);
        SetObjectPosition(bulletCase, bulletCasePoint);

        //Instantiate(bulletObject, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up ));
        //총알 생성해서 사격하는 부분
        GameObject prefabToSpawn = PoolManager.instance.ActivateObject(0);
        SetObjectPosition(prefabToSpawn, bulletPoint);
        prefabToSpawn.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);

        //레이캐스트를 이용하여 사격하는 부분
        //if (enemy && enemy.enemyCurrentHP > 0)
        //{
        //    enemy.enemyCurrentHP -= 1;
        //    Debug.Log("Enemy Hit! Remaining HP: " + enemy.enemyCurrentHP);
        //}

    }

    public void ReloadClip()
    {
        //Instantiate(weaponClipFX, weaponClipPoint);
        GameObject weaponClip = PoolManager.instance.ActivateObject(3);
        SetObjectPosition(weaponClip, weaponClipPoint);

        initBullet();
    }

    void initBullet()
    {
        currentBullet = maxBullet;
    }

    void SetObjectPosition(GameObject obj, Transform targetTransform)
    {
        obj.transform.position = targetTransform.position;
    }
    IEnumerator EnemySpawn()
    {
        //Instantiate(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
        GameObject enemy = PoolManager.instance.ActivateObject(4);
        SetObjectPosition(enemy, spawnPoint[Random.Range(0, spawnPoint.Length)].transform);

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemySpawn());
    }
}
