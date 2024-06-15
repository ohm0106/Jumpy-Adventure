using UnityEngine;

public interface IPlayerTrigger
{
    void OnPlayerEnter(Player player);
    void OnPlayerExit(Player player);
}
