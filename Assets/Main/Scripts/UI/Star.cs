using UnityEngine;
using UnityEngine.UI;
public class Star : MonoBehaviour
{
    bool isActive;

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(Sprite sprite)
    {
        if (isActive)
            return;
        GetComponent<Image>().sprite = sprite;
        isActive = true;
    }

}
