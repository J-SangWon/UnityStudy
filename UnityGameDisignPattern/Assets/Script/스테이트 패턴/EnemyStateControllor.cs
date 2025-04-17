//using UnityEngine;
////    5. 스테이트(State) 패턴
////스테이트 패턴은 객체의 내부 상태가 변경될 때 동작이 변경되도록 하는 패턴입니다.
////Unity에서는 캐릭터나 적의 AI 상태 관리에 매우 유용합니다.

//public interface IEnemyState
//{
//    void EnterState(EnemyStateControllor enemy);
//    void UpdateState(EnemyStateControllor enemy);
//    void ExitState(EnemyStateControllor enemy);
//    void OntriggerState(EnemyStateControllor enemy, Collider other);
//}

//public class PatrolState : IEnemyState
//{
//    private float _patrollTimer = 0f;
//    private int _currentWaypointIndex = 0;
//    public void EnterState(EnemyStateControllor enemy)
//    {
//        Debug.Log("순찰 상태");
//        if(enemy.waypoints.Length > 0)
//        {
//            enemy.navMeshAgent.SetDes
//        }
//    }

//    public void ExitState(EnemyStateControllor enemy)
//    {
//        throw new System.NotImplementedException();
//    }

//    public void OntriggerState(EnemyStateControllor enemy, Collider other)
//    {
//        throw new System.NotImplementedException();
//    }

//    public void UpdateState(EnemyStateControllor enemy)
//    {
//        throw new System.NotImplementedException();
//    }
//}

//public class EnemyStateControllor : MonoBehaviour
//{
//    void Start()
//    {
        
//    }

//    void Update()
//    {
        
//    }
//}
