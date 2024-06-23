using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : Singleton<MissionManager>
{
    [SerializeField]
    Dictionary<string , MissionItem> subscribeItemMission; // ���� ��ϵ� ������ �̼�

    [SerializeField]
    Dictionary<string, MissionEnemy> subscribeEnemyMission;// ���� ��ϵ� �� ���� �̼�

    [SerializeField]
    TMP_Text missionText;

    DialogueManager dialogueManager;
    UIToggleWindow uiToggleWindow;

    [SerializeField]
    PlayerEvent eventP;// todo

    void Start()
    {
        subscribeItemMission = new Dictionary<string, MissionItem>();
        subscribeEnemyMission = new Dictionary<string, MissionEnemy>();
        dialogueManager = FindAnyObjectByType<DialogueManager>();
        uiToggleWindow = FindAnyObjectByType<UIToggleWindow>();
    }

    void OnEnable()
    {
        // ������
        eventP.OnAddItem += SubscribeAddItem; 
        eventP.OnDeleteItem += SubscribeDeleteItem;
    }

    void OnDisable()
    {
        // ������
        eventP.OnAddItem -= SubscribeAddItem;
        eventP.OnDeleteItem -= SubscribeDeleteItem;
    }

    public bool StartItemMission(MissionItem mission)
    {
        if (!subscribeItemMission.ContainsKey(mission.missionName))
        {
            subscribeItemMission.Add(mission.missionName,mission);
            dialogueManager.StartDialogue(mission.conversions);
            StartCoroutine(CoStartMission(mission.missionName));
        }

        return true;
    }

    public bool StartEnemyMission(MissionEnemy mission)
    {
        if (!subscribeItemMission.ContainsKey(mission.missionName))
        {
            subscribeEnemyMission.Add(mission.missionName, mission);
            dialogueManager.StartDialogue(mission.conversions);
            StartCoroutine(CoStartMission(mission.missionName));
        }

        return true;
    }

    private IEnumerator CoStartMission(string missionName)
    {
        yield return new WaitUntil(() => !dialogueManager.GetIsDialogue());

        uiToggleWindow.UpdateWindowText(missionName);
    }

    private IEnumerator CoStartComplete(string missionName)
    {
        yield return new WaitUntil(() => !dialogueManager.GetIsDialogue());

        uiToggleWindow.UpdateWindowText(missionName);
    }

    public void CheckMissionComplete(string missionName)
    {
        if(subscribeItemMission.ContainsKey(missionName)&&
           subscribeItemMission[missionName].condition.IsConditionMet())
        {
            dialogueManager.StartDialogue(subscribeItemMission[missionName].completeConversations);
        }

        //todo : add Enemy Type
    }

    void SubscribeAddItem(ItemType itemType, int amount)
    {
        if (subscribeItemMission.Count == 0)
            return;

        foreach(var mission in subscribeItemMission.Values)
        {
             mission.condition.SetCurrent(itemType,amount);
        }
    }

    void SubscribeDeleteItem(ItemType itemType, int amount)
    {
        if (subscribeItemMission.Count == 0)
            return;

        foreach (var mission in subscribeItemMission.Values)
        {
            mission.condition.SetCurrent(itemType,amount);
        }
    }
}
