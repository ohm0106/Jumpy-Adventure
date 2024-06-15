using UnityEngine;

public class Ladder : MonoBehaviour, IPlayerTrigger
{
    public void OnPlayerEnter(Player player)
    {
        player.SetClimbing(this.transform.position);
    }

    public void OnPlayerExit(Player player)
    {
        player.ReleseClimbing();

    }

}