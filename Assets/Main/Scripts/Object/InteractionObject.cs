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
    ObjectAction alwaysAction; // 사용자 가까이 올 시 
    [Header("[ 근처 가까이 올 시 Action ]")]
    [SerializeField]
    ObjectAction distanceAction; // 사용자 가까이 올 시 
    [Header("[ Player 상호작용시 Action ]")]
    [SerializeField]
    ObjectAction clickAction; // 사용자 클릭 시 

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
        isAround = true;
        player.SetInteract(this);
        Debug.Log("Player interacted with object.");

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
        Debug.Log("ReleaseAroundPerform Release.");

        if (distanceAction)
            distanceAction.ReleaseAction();

    }

    public void ReleaseClickPerformAction()
    {

        Debug.Log("ReleaseClickPerform Release.");

        if (clickAction)
            clickAction.ReleaseAction();
    }

    public InteractionObjectType SetPlayerInteraction()
    {
        return type;
    }

    #endregion
}