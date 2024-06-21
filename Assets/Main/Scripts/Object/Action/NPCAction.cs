using System.Collections;
using UnityEngine;
using System;
using System.Linq;

public class NPCAction : ObjectAction
{
    [SerializeField]
    Mission mission1;

    [SerializeField]
    Mission mission2;

    [SerializeField]
    Mission mission3;

    public void SetMission()
    {
        MissionManager.Instance.StartMission(mission1);
    }

    public override IEnumerator CoAction()
    {
        yield return StartCoroutine(base.CoAction());

        SetMission();
    }
}
