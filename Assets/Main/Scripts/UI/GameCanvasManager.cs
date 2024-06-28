using System;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : Singleton<GameCanvasManager>
{
    [SerializeField]
    SliderController hpController;
    [SerializeField]
    enemySliderController enemyHpController;
    [SerializeField]
    StarUIController starUIController;
    [SerializeField]
    public  PlayerEvent playerEvent; // todo 

    [SerializeField]
    public event Action<EnemyType, int, int> onEnemyHPBar;   

    private void OnDisable()
    {
        if (playerEvent != null)
        {
            playerEvent.OnPlayerHP -= hpController.UpdateSliderUI;
        }
    }
    private void OnEnable()
    {
        if (playerEvent != null)
        {
            playerEvent.OnPlayerHP += hpController.UpdateSliderUI;
        }
    }

    public void SetEnemyHPBar(EnemyType type, int curHp, int maxHp)
    {
        onEnemyHPBar.Invoke(type, curHp, maxHp);
    }

}
