using UnityEngine;


public enum ItemType
{
    NONE,
    STAR,
    KEY,
    APPLE
}

[System.Serializable]
public class Item 
{
    public ItemType itemType;
    public Sprite itemSprite;
}