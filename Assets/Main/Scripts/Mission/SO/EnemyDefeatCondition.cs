using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewEnemyDefeatCondition", menuName = "MissionConditions/EnemyDefeat")]
public class EnemyDefeatCondition : Condition<EnemyType>
{
    [SerializeField]
    EnemyType requirEnemyType;

    public override MissionType GetMissionType()
    {
        return MissionType.ENEMY;
    }

    public override void SetCurrent(EnemyType value, int amount)
    {
        if (requirEnemyType == value)
        {
            currentAmount += amount;

        }
    }
}

public enum EnemyType
{
    NONE
}