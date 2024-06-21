using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : Singleton<MissionManager>
{
    [SerializeField]
    List<Mission> subScribeMission; // 현재 등록된 미션

    [SerializeField]
    TMP_Text missionText;

    DialogueManager dialogueManager;
    UIToggleWindow uiToggleWindow;

    void Start()
    {
        subScribeMission = new List<Mission>();
        dialogueManager = FindAnyObjectByType<DialogueManager>();
        uiToggleWindow = FindAnyObjectByType<UIToggleWindow>();
    }

    public void StartMission(Mission mission)
    {
        subScribeMission.Add(mission);
        dialogueManager.StartDialogue(mission.conversions);

        StartCoroutine(CoStartMission(mission));

    }

    private IEnumerator CoStartMission(Mission mission)
    {
        yield return new WaitUntil(() => !dialogueManager.GetIsDialogue());

        uiToggleWindow.UpdateWindowText(mission.missionName);
    }



}
