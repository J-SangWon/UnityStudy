using System.Collections;
using UnityEngine;

public class GettingHitState : State<EnemyController>
{
    EnemyController enemy;

    [SerializeField] float stunTime = 0.5f;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;
        enemy.fighter.OnHitComplete += () => StartCoroutine(GoToCombatMovement());
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    IEnumerator GoToCombatMovement()
    {
        yield return new WaitForSeconds(stunTime);
        enemy.ChangeState(EnemyStates.CombatMovement);
    }



}
