using UnityEngine;

public enum InteractionObjectType
{
    NONE,
    STAR,
    HOUSE,
    NPC,
    Ladder
}


public class InteractionObject : MonoBehaviour, IPlayerAction
{
    [SerializeField]
    float interactionDistance = 1.5f;
    bool isAround;

    [Header("[ 조건 없이 Action ]")]
    [SerializeField]
    ObjectAction alwaysAction; 
    [Header("[ 근처 가까이 올 시 Action ]")]
    [SerializeField]
    ObjectAction distanceAction; 
    [Header("[ Player 상호작용시 Action ]")]
    [SerializeField]
    ObjectAction clickAction; 

    Player player;
    [SerializeField]
    InteractionObjectType type;

    void Start()
    {
        player = FindObjectOfType<Player>(); // [Todo] : 나중에 변경할 것. 

        if(alwaysAction)
            alwaysAction.StartAction();
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
        else if(distance > interactionDistance && isAround)
        {
            ReleaseAroundPerformAction();
        }
    }

    #region PlayerActionInterface
    public void AroundPerformAction()
    {
        if (clickAction)
            player.SetInteract(this);

        isAround = true;
        if(distanceAction)
            distanceAction.StartAction();
    }

    public void ClickPerformAction()
    {
        if (clickAction)
        {
            clickAction.StartAction();
        }

    }

    public void ReleaseAroundPerformAction()
    {
        isAround = false;
        player.SetInteract(null);

        if (distanceAction)
            distanceAction.ReleaseAction();

    }

    public void ReleaseClickPerformAction()
    {
        if (clickAction)
            clickAction.ReleaseAction();
    }

    public InteractionObjectType SetPlayerInteraction()
    {
        return type;
    }

    #endregion
}