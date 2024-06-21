using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewEnemyDefeatCondition", menuName = "MissionConditions/EnemyDefeat")]
public class EnemyDefeatCondition : ScriptableObject, MissionCondition
{
    public string enemyType; // todo 추후 에 적 타입이 확정되면 enum 으로 변경할 것. 
    public int requiredDefeatCount;
    private int currentDefeatCount;

    public bool IsConditionMet()
    {
        return currentDefeatCount >= requiredDefeatCount;
    }
    public void SetItemCurrentSet(int degree)
    {
        currentDefeatCount += degree;
    }
    public void DefeatEnemy(string type)
    {
        if (type == enemyType)
        {
            currentDefeatCount++;
        }
    }
}