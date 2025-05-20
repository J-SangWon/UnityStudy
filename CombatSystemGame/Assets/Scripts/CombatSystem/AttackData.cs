using UnityEngine;

[CreateAssetMenu(menuName = "Combat System / Create a new Attack")]
public class AttackData : ScriptableObject
{
    [field : SerializeField] public string animName { get; private set; }
    [field : SerializeField] public AttackHitBox HitboxToUse { get; private set; }
    [field : SerializeField] public float impactStartTime { get; private set; }
    [field : SerializeField] public float impactEndTime { get; private set; }

}

public enum AttackHitBox { LeftHand, RightHand, LeftFoot, RightFoot, Sword }; 