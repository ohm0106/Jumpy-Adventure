using System;
using UnityEngine;

public abstract class Condition<TEnum> : ScriptableObject where TEnum : Enum
{
    [SerializeField]
    int requiredAmount;
    protected int currentAmount;

    public bool IsConditionMet()
    {
        return currentAmount >= requiredAmount;
    }

    public abstract void SetCurrent(TEnum value, int amount);

    public abstract MissionType GetMissionType();

}
