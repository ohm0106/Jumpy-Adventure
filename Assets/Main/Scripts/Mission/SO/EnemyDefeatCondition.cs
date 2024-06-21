using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewEnemyDefeatCondition", menuName = "MissionConditions/EnemyDefeat")]
public class EnemyDefeatCondition : ScriptableObject, MissionCondition
{
    public string enemyType; // todo ���� �� �� Ÿ���� Ȯ���Ǹ� enum ���� ������ ��. 
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