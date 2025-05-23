using System.Collections;
using UnityEngine;

public class EnemyAttackState : State<EnemyController>
{
    EnemyController enemy;

    [SerializeField] float attackDistance = 1f;
    bool isAttacking;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;

        enemy.NavAgent.stoppingDistance = attackDistance;
    }

    public override void Execute()
    {
        if (isAttacking)
            return;

        enemy.NavAgent.SetDestination(enemy.Target.transform.position);
        if (Vector3.Distance(enemy.Target.transform.position, enemy.transform.position) <= attackDistance + 0.03f)
        {
            StartCoroutine(Attack(Random.Range(0, enemy.fighter.GetAttackDatas.Count+1)));
        }
    }

    IEnumerator Attack(int comboCount = 1)
    {
        isAttacking = true;
        enemy.anim.applyRootMotion = true;

        enemy.fighter.TryToAttack();

        for (int i = 1; i < comboCount; i++) 
        {
            yield return new WaitUntil(() => enemy.fighter.attackState == AttackState.Cooldown);
            enemy.fighter.TryToAttack();
        }

        yield return new WaitUntil(() => enemy.fighter.attackState == AttackState.Idle);

        enemy.anim.applyRootMotion = false;
        isAttacking = false;
        if(enemy.IsInState(EnemyStates.Attack))
        enemy.ChangeState(EnemyStates.RetreatAfterAttack);
    }
    public override void Exit()
    {
        enemy.NavAgent.ResetPath();
    }
}
