using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MissionComplete : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    Image[] twinkle;

    [SerializeField]
    Image[] lines;

    [SerializeField]
    GameObject mainText;

    [SerializeField]
    TMP_Text middleText;


    public void StartEffect(string mText)
    {
        middleText.text = "-" + mText + "-";
        panel.SetActive(true);

        StartCoroutine(CoStartEffect());

    }

    IEnumerator CoStartEffect()
    {

        foreach (var img in twinkle)
        {
            img.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo); 
        }

        middleText.transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(3f);


        foreach (var img in twinkle)
        {
            img.DOKill();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1); 
        }

        middleText.transform.DOKill();
        middleText.transform.localScale = Vector3.one;

        panel.SetActive(false);

        yield return null;
    }


}
