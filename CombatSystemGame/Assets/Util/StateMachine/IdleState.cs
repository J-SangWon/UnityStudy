using UnityEngine;

public class IdleState : State<EnemyController>
{
    EnemyController enemy;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;
        enemy.anim.SetBool("combatMode", false);

    }
    public override void Execute()
    {
        enemy.Target = enemy.FindTarget();
    }

    public override void Exit()
    {

    }

}
