using UnityEngine;

public enum InteractionObjectType
{
    STAR,
    HOUSE,
    SPIDERWEB
}


public class InteractionObject : MonoBehaviour, PlayerActionInterface
{
    [SerializeField]
    float interactionDistance = 1.5f;
    bool isInteraction;

    [SerializeField]
    ObjectAction action;

    Player player;
    [SerializeField]
    InteractionObjectType type;

    void Start()
    {
        player = FindObjectOfType<Player>(); // [Todo] : 나중에 변경할 것. 
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
        if (distance <= interactionDistance)
        {
            PerformAction(player);
        }
        else
        {
            ReleaseAction();
        }
    }

    #region PlayerActionInterface
    public void PerformAction(Player player)
    {
        isInteraction = true;
        player.SetInteract(this);
        Debug.Log("Player interacted with object.");

        if(action)
            action.StartAction();
    }

    public void ReleaseAction()
    {
        isInteraction = false;
        player.SetInteract(null);
        Debug.Log("Interaction Release.");

        if (action)
            action.ReleaseAction();

    }

    public InteractionObjectType SetPlayerInteraction()
    {
        return type;
    }

    #endregion
}