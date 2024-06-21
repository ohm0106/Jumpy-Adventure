using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Mission")]
public class Mission : ScriptableObject
{
    public string missionName;
    public Conversation[] conversions;
    public bool isCompleted;
    public MissionCondition conditions;

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

