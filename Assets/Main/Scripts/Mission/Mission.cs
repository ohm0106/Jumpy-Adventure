using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Mission")]
public class Mission : ScriptableObject
{
    public string missionName;
    public conversation[] conversions;
    public bool isCompleted;
    public MissionCondition conditions;

    public void CompleteMission()
    {
        if (IsConditionsMet())
        {
            isCompleted = true;
        }
    }

    public bool IsConditionsMet()
    {
        if (!conditions.IsConditionMet())
        {
            return false;
        }
        return true;
    }
}

[System.Serializable]
public class conversation
{
    public string[] npcConversation;

    public string[] playerConversation;


}

