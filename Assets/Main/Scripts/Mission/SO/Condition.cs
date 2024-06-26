using System;
using UnityEngine;

public abstract class Condition<TEnum> : ScriptableObject where TEnum : Enum
{
    [SerializeField]
    protected int requiredAmount;
    protected int currentAmount;


    public void Init()
    {
        currentAmount = 0;
    }

    public bool IsConditionMet()
    {
        return currentAmount >= requiredAmount;
    }

    public abstract void SetCurrent(TEnum value, int amount);

    public abstract MissionType GetMissionType();

    public int GetCurrentAmount()
    {
        return currentAmount;
    }

}
