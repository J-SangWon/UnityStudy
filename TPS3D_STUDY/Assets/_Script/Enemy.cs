using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Slider HPBar;
    public float enemyCurrentHP;
    public float enemyMaxHP = 100f;

    NavMeshAgent agent;
    Animator anim;

    GameObject targetPlayer;
    float targetDelay;

    CapsuleCollider col;
    void Start()
    {
        InitEnemyHP();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float maxDelay = 0.5f;
        targetDelay += Time.deltaTime;
        if (targetDelay < maxDelay)
        {
            return;
        }

        HPBar.value = enemyCurrentHP / enemyMaxHP;

        if (enemyCurrentHP <= 0)
        {
            StartCoroutine(EnemyDie());
        }

        if (targetPlayer)
        {
            agent.destination = targetPlayer.transform.position;
            transform.LookAt(targetPlayer.transform.position);

            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <= agent.stoppingDistance;

            if (isRange)
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                anim.SetFloat("MoveSpeed", agent.velocity.magnitude);

            }

            targetDelay = 0f;
        }
    }

    void InitEnemyHP()
    {
        enemyCurrentHP = enemyMaxHP;
    }

    IEnumerator EnemyDie()
    {
        agent.speed = 0;
        anim.SetTrigger("Dead");
        col.enabled = false;

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
        InitEnemyHP();
        agent.speed = 1;
        col.enabled = true;
    }
}
