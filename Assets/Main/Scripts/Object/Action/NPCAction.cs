using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class NPCAction : ObjectAction
{
    [SerializeField]
    MissionItem mission1;

    [SerializeField]
    MissionItem mission2;

    [SerializeField]
    MissionItem mission3;

    bool isMission;
    Queue<MissionItem> missionQ;

    private void Start()
    {
        InitializeMissionQueue();
    }

    private void InitializeMissionQueue()
    {
        missionQ = new Queue<MissionItem>();

        if (mission1 != null)
            missionQ.Enqueue(mission1);
        if (mission2 != null)
            missionQ.Enqueue(mission2);
        if (mission3 != null)
            missionQ.Enqueue(mission3);
    }

    public void SetMission()
    {
        if (missionQ.Count > 0)
        {
            MissionItem currentMission = missionQ.Peek();
            isMission = MissionManager.Instance.StartItemMission(currentMission);
        }
        else
        {
            isMission = false;
        }
    }

    public override IEnumerator CoAction()
    {
        PlayerEvent eventC = FindObjectOfType<PlayerEvent>(); // Todo
        eventC.SetMovePlayer(false);

        if (!isMission)
        {
            SetMission();
        }
        else
        {
            MissionItem currentMission = missionQ.Peek();
            bool isComplete = MissionManager.Instance.CheckMissionComplete(currentMission.missionName);

            if (isComplete)
            {
                missionQ.Dequeue(); // Remove the completed mission from the queue
                isMission = false; // Reset mission flag to start the next mission
            }
        }

        isAction = false;
        yield return null;
    }
}
