using UnityEngine;
using UnityEngine.UI;

public class enemySliderController : SliderController
{
    [SerializeField]
    GameObject slider;


    [SerializeField]
    Image enemyIMG;

    [SerializeField]
    enemyHPImgBarUI[] hpUI;
    private void Start()
    {
        Show(false);
    }
    void OnEnable()
    {
        GameCanvasManager.Instance.onEnemyHPBar += SetEnemyHPBar;
    }
    private void OnDisable()
    {
        GameCanvasManager.Instance.onEnemyHPBar -= SetEnemyHPBar;
    }

    private void SetEnemyHPBar(EnemyType type, int curhp, int maxhp)
    {
        foreach (var arr in hpUI)
        {
            if (arr.type == type)
            {
                enemyIMG.sprite = arr.ui;
                break;
            }
        }
        //img ¼ÂÆÃ 
        UpdateSliderUI(curhp, maxhp);
    }
    public void Show(bool isVisible)
    {
        slider.SetActive(isVisible);
    }
}

[System.Serializable]
public class enemyHPImgBarUI
{
    public Sprite ui;
    public EnemyType type;
}