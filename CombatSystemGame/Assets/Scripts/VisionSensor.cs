using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    [SerializeField] EnemyController enemy;

    private void Awake()
    {
        enemy.visionSensor = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        var fighter = other.GetComponent<MeeleFighter>();
        if (fighter)
        {
            enemy.TargetsInRange.Add(fighter);
            EnemyManager.instance.AddEnemyInRange(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var fighter = other.GetComponent<MeeleFighter>();
        if (fighter)
        {
            enemy.TargetsInRange.Remove(fighter);
            EnemyManager.instance.RemoveEnemyInRange(enemy);
        }
    }

}
