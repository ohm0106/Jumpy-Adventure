
using UnityEngine;

public interface PlayerActionInterface
{
    Renderer GetRenderer();
    void AroundPerformAction();
    void ClickPerformAction();
    void ReleaseAroundPerformAction();
    void ReleaseClickPerformAction();
    InteractionObjectType SetPlayerInteraction();

}
