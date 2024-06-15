using UnityEngine;

public class Water : MonoBehaviour, IPlayerTrigger
{

    [SerializeField]
    GameObject paticlePrefab;

    public void OnPlayerEnter(Player player)
    {
        Instantiate(paticlePrefab, player.transform.position, player.transform.rotation);
        player.Damage(100);
        
    }

    public void OnPlayerExit(Player player)
    {
        // �ʿ� �� �߰� ó��
    }
}