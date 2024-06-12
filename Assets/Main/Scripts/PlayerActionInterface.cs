
public interface PlayerActionInterface
{
    void PerformAction(Player player);

    void ReleaseAction();

    InteractionObjectType SetPlayerInteraction();
}
