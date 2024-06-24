using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class UIToggleWindow : MonoBehaviour
{
    [SerializeField]
    RectTransform windowPanel; 
    [SerializeField]
    TMP_Text windowText; 
    [SerializeField]
    Button toggleButton; 

    bool isExpanded = true;
    float expandedHeight;
    float collapsedHeight = 0f;

    List<string> missionText;

    void Start()
    {
        // 초기화
        missionText = new List<string>();
        expandedHeight = windowPanel.sizeDelta.y;
        toggleButton.onClick.AddListener(ToggleWindow);

        if (isExpanded)
            ToggleWindow();
    }

    void ToggleWindow()
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

    public void DeleteWindowText(string alreadyText)
    {

        if (!isExpanded)
            ToggleWindow();
    }
}