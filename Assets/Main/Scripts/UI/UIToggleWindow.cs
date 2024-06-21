using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIToggleWindow : MonoBehaviour
{
    [SerializeField]
    private RectTransform windowPanel; 
    [SerializeField]
    private TMP_Text windowText; 
    [SerializeField]
    private Button toggleButton; 

    private bool isExpanded = true;
    private float expandedHeight;
    private float collapsedHeight = 0f; 

    void Start()
    {
        // 초기화
        expandedHeight = windowPanel.sizeDelta.y;
        toggleButton.onClick.AddListener(ToggleWindow);

        if (isExpanded)
            ToggleWindow();
    }

    private void ToggleWindow()
    {
        if (isExpanded)
        {
            // 창 접기
            windowPanel.DOSizeDelta(new Vector2(windowPanel.sizeDelta.x, collapsedHeight), 0.5f).SetEase(Ease.InOutQuad);
            windowPanel.DOAnchorPosY(windowPanel.anchoredPosition.y + expandedHeight / 2, 0.5f).SetEase(Ease.InOutQuad);
            toggleButton.GetComponent<RectTransform>().localScale = new Vector3(1, -1, 1);

        }
        else
        {
            // 창 펼치기
            windowPanel.DOSizeDelta(new Vector2(windowPanel.sizeDelta.x, expandedHeight), 0.5f).SetEase(Ease.InOutQuad);
            windowPanel.DOAnchorPosY(windowPanel.anchoredPosition.y - expandedHeight / 2, 0.5f).SetEase(Ease.InOutQuad);
            toggleButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        isExpanded = !isExpanded;
    }


    public void UpdateWindowText(string newText)
    {
        windowText.text = newText;
        if (!isExpanded)
            ToggleWindow();
    }
}