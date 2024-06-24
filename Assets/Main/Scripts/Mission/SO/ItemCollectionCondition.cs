using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewItemCollectionCondition", menuName = "MissionConditions/ItemCollection")]
public class ItemCollectionCondition : Condition<ItemType>
{
    [SerializeField]
    ItemType requiredItemName;
    
    public override MissionType GetMissionType()
    {
        return MissionType.ITEM;
    }

    public override void SetCurrent(ItemType value, int amount)
    {
        if(requiredItemName == value)
        {
            currentAmount += amount;
        }
    }
}