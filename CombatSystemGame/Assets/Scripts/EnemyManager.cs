using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Vector2 timeRangeBetweenAttacks = new Vector2(1, 4);
    [SerializeField] CombatController player;
    public static EnemyManager instance { get; private set; }

    float timer = 0f;

    private void Awake()
    {
        instance = this;
    }



    public List<EnemyController> enemiesInRange = new List<EnemyController>();
    float notAttackingTimer = 2;

    public void AddEnemyInRange(EnemyController enemy)
    {
        if (!enemiesInRange.Contains(enemy))
            enemiesInRange.Add(enemy);
    }


    public void RemoveEnemyInRange(EnemyController enemy)
    {
        enemiesInRange.Remove(enemy);

        if (enemy == player.TargetEnemy)
        {
            enemy.MeshHighlighter?.HighlightMesh(false);
            player.TargetEnemy = GetClosesEnemyToPlayerDir();
        }

    }

    private void Update()
    {
        if (enemiesInRange.Count == 0) return;

        if (!enemiesInRange.Any(e => e.IsInState(EnemyStates.Attack)))
        {
            if (notAttackingTimer > 0)
            {
                notAttackingTimer -= Time.deltaTime;
            }

            var attackingEnemy = SelectenemyForAttack();
            if (notAttackingTimer <= 0)
            {
                attackingEnemy.ChangeState(EnemyStates.Attack);
                notAttackingTimer = Random.Range(timeRangeBetweenAttacks.x, timeRangeBetweenAttacks.y);
            }
        }
        if (timer > 0.1f)
        {
            timer = 0f;
            var closestEnemy = GetClosesEnemyToPlayerDir();
            if (closestEnemy != null && closestEnemy != player.TargetEnemy)
            {
                var prevEnemy = player.TargetEnemy;
                player.TargetEnemy = closestEnemy;
                player?.TargetEnemy?.MeshHighlighter.HighlightMesh(true);
                prevEnemy?.MeshHighlighter.HighlightMesh(false);
            }
        }

        timer += Time.deltaTime;
    }
    EnemyController SelectenemyForAttack()
    {
        return enemiesInRange.OrderByDescending(e => e.combatMovementTimer)
            .FirstOrDefault(e => e.Target != null && e.IsInState(EnemyStates.CombatMovement));
    }

    public EnemyController GetAttackingEnemy()
    {
        return enemiesInRange.FirstOrDefault(e => e.IsInState(EnemyStates.Attack));
    }

    public EnemyController GetClosesEnemyToPlayerDir()
    {
        var targetingDir = player.GetTargetingDir();
        float minDistance = Mathf.Infinity;
        EnemyController closestEnemy = null;
        foreach (var enemy in enemiesInRange)
        {
            var vecToEnemy = enemy.transform.position - player.transform.position;
            vecToEnemy.y = 0f; // 수평 방향으로만 비교

            float angle = Vector3.Angle(targetingDir, vecToEnemy);
            float distance = vecToEnemy.magnitude * Mathf.Sin(angle * Mathf.Deg2Rad);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

}
