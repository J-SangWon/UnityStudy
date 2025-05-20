using UnityEngine;

public class ChaseState : State<EnemyController>
{
    EnemyController enemy;
    public override void Enter(EnemyController owner)
    {
        enemy = owner;
        Debug.Log("ChaseEnter");
    }
    public override void Execute()
    {
        //base.Execute();
        Debug.Log("ChaseExcute");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("ChaseExit");
    }
}
