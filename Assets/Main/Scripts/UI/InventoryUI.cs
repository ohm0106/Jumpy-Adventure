using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text countT;

    [SerializeField]
    Image image;

    int num;

    bool isEmpty;

    void Start()
    {
        Clear();
    }

     void Clear()
    {
        countT.text = "";
        num = 0;
        image.enabled = false;
        isEmpty = true;
    }

    public void SetImageAndText(Sprite sprite, int amount)
    {
        image.enabled = true;
        isEmpty = false;
        image.sprite = sprite;
        num += amount;
        countT.text = $"{num}";
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public bool SetText(int amount)
    {
        if(amount + num <= 0)
        {
            Clear();
            return true;
        }
        else
        {
            num += amount;
            countT.text = $"{num}";
            return false;
        }
        
    }
}
