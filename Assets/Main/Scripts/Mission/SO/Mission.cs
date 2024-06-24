using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionItem", menuName = "Mission/MissionItem")]
public class MissionItem : ScriptableObject
{
    public string missionName;
    public string detailName;
    public Conversation[] conversions;
    public Conversation[] completeConversations;
    public bool isCompleted;
    public Condition<ItemType> condition;

}
[CreateAssetMenu(fileName = "NewMissionEnemy", menuName = "Mission/MissionEnemy")]
public class MissionEnemy : ScriptableObject
{
    public string missionName;
    public string detailName;
    public Conversation[] conversions;
    public Conversation[] completeConversations;
    public bool isCompleted;
    public Condition<EnemyType> condition;

}

public enum conversionType
{
    Player,
    NPC
}

[System.Serializable]
public class Conversation
{
    public string name;

    public string talk;
}

