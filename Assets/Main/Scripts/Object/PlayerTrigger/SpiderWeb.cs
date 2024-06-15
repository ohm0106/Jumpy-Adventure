using UnityEngine;

public class SpiderWeb : MonoBehaviour, IPlayerTrigger
{
    [SerializeField]
    float speedDebuff = 3f;

    public void OnPlayerEnter(Player player)
    {
        player.SlowSpeed(speedDebuff);
    }

    public void OnPlayerExit(Player player)
    {
        player.ResetSpeed();
    }
}