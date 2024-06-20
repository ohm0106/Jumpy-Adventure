using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewItemCollectionCondition", menuName = "MissionConditions/ItemCollection")]
public class ItemCollectionCondition : ScriptableObject, MissionCondition
{
    public ItemType requiredItemName;
    public int requiredAmount;
    private int currentAmount;

    public bool IsConditionMet()
    {
        return currentAmount >= requiredAmount;
    }

    public void CollectItem(ItemType itemName, int amount)
    {
        if (itemName == requiredItemName)
        {
            currentAmount += amount;
        }
    }
}