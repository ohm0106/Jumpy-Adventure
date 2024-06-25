using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    [SerializeField]
    Image hpBar;

    [SerializeField]
    TMP_Text hpText;

    [SerializeField]
    PlayerEvent playerEvent; //todo 

    void Start()
    {
    }

    private void OnDestroy()
    {
        if (playerEvent != null)
        {
            playerEvent.OnPlayerHP -= UpdateHPUI;
        }
    }

    private void OnEnable()
    {
        if (playerEvent != null)
        {
            playerEvent.OnPlayerHP += UpdateHPUI;
        }
    }

    private void UpdateHPUI(int currentHP, int maxHP)
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
