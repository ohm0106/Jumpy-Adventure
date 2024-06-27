using UnityEngine;
using System.Collections;

public class InteractionsObject : MonoBehaviour, IPlayerAction
{
    [SerializeField]
    float interactionDistance = 1.5f;
    bool isAround;

    [Header("[ ���� ���� Action ]")]
    [SerializeField]
    public ObjectAction[] alwaysAction;
    [Header("[ ��ó ������ �� �� Action ]")]
    [SerializeField]
    ObjectAction[] distanceAction;
    [Header("[ Player ��ȣ�ۿ�� Action ]")]
    [SerializeField]
    ObjectAction[] clickAction;

    Player player;
    [SerializeField]
    InteractionObjectType type;

    void Start()
    {
        player = FindObjectOfType<Player>(); // [Todo] : ���߿� ������ ��. 

        if (alwaysAction.Length > 0)
        {
            foreach(var arr in alwaysAction)
                arr.StartAction();
        }
            
    }

    void Update()
    {
        CheckNearPlayer();
    }

    void CheckNearPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene.");
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionDistance && !isAround)
        {
            AroundPerformAction();
        }
        else if (distance > interactionDistance && isAround)
        {
            ReleaseAroundPerformAction();
        }
    }

    #region PlayerActionInterface
    public void AroundPerformAction()
    {
        if (clickAction.Length > 0)
            player.SetInteract(this);

        isAround = true;
        if (distanceAction.Length > 0)
        {
            foreach (var arr in distanceAction)
                arr.StartAction();
        }
    }

    public void ClickPerformAction()
    {
        if (clickAction.Length > 0)
        {
            StartCoroutine(CoPerformActionInorder());
        }
    }

    IEnumerator CoPerformActionInorder()
    {
        player.GetComponent<PlayerEvent>().SetMovePlayer(false);
        foreach (var arr in clickAction)
        {
            yield return arr.StartActionReturn();
        }
        player.GetComponent<PlayerEvent>().SetMovePlayer(true);
    }

    public void ReleaseAroundPerformAction()
    {
        isAround = false;
        player.SetInteract(null);

        if (distanceAction.Length > 0)
        {
            foreach (var arr in distanceAction)
                arr.ReleaseAction();
        }

    }

    public void ReleaseClickPerformAction()
    {
        if (clickAction.Length > 0)
        {
            foreach (var arr in clickAction)
                arr.ReleaseAction();
        }
    }

    public InteractionObjectType SetPlayerInteraction()
    {
        return type;
    }

    #endregion
}