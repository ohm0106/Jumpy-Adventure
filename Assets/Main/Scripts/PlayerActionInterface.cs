
using UnityEngine;

public interface IPlayerAction
{
    void AroundPerformAction();
    void ClickPerformAction();
    void ReleaseAroundPerformAction();
    void ReleaseClickPerformAction();
    InteractionObjectType SetPlayerInteraction();

}
