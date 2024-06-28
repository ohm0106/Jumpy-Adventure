using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    [SerializeField]
    protected Image hpBar;

    [SerializeField]
    protected TMP_Text hpText;

    public void UpdateSliderUI(int currentHP, int maxHP)
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)currentHP / maxHP;
        }

        if (hpText != null)
        {
            hpText.text = $"{currentHP} / {maxHP}";
        }
    }

}

