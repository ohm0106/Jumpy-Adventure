using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TelePortAction : ObjectAction
{
    [SerializeField]
    Transform teleportPosition; 
    [SerializeField]
    float teleportDuration = 1.0f; 
    [SerializeField]
    float moveHeight = 2.0f; 

    bool isTeleporting = false; 

    public override void StartAction()
    {
        base.StartAction();

    }

    public override IEnumerator CoAction()
    {
        if (isTeleporting)
            yield return null;

        isTeleporting = true;

        PlayerEvent eventC =  FindObjectOfType<PlayerEvent>(); // Todo
        eventC.SetMoveTeleportPlayer(false);

        eventC.transform.DOMoveY(eventC.transform.position.y + moveHeight, teleportDuration / 2);
        yield return new WaitForSeconds(teleportDuration / 2);

        eventC.transform.position = new Vector3(teleportPosition.position.x, eventC.transform.position.y, teleportPosition.position.z);

        eventC.transform.DOMoveY(teleportPosition.position.y, teleportDuration / 2);
        yield return new WaitForSeconds(teleportDuration / 2);

        // reset
        eventC.SetMoveTeleportPlayer(true); // Todo
        isTeleporting = false;
        isAction = false;

    }
}
