using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : Singleton<MissionManager>
{
    [SerializeField]
    Dictionary<string , MissionItem> subscribeItemMission; // 현재 등록된 아이템 미션

    [SerializeField]
    Dictionary<string, MissionEnemy> subscribeEnemyMission;// 현재 등록된 적 제거 미션

    [SerializeField]
    TMP_Text missionText;

    [SerializeField]
    DialogueManager dialogueManager;
    [SerializeField]
    UIToggleWindow uiToggleWindow;
    [SerializeField]
    MissionComplete missionComplete;

    [SerializeField]
    PlayerEvent eventP;// todo

    void Start()
    {
        subscribeItemMission = new Dictionary<string, MissionItem>();
        subscribeEnemyMission = new Dictionary<string, MissionEnemy>();
    }

    void OnEnable()
    {
        // 아이템
        eventP.OnAddItem += SubscribeAddItem; 
        eventP.OnDeleteItem += SubscribeDeleteItem;
    }

    void OnDisable()
    {
        // 아이템
        eventP.OnAddItem -= SubscribeAddItem;
        eventP.OnDeleteItem -= SubscribeDeleteItem;
    }

    public bool StartItemMission(MissionItem mission)
    {
        if (!subscribeItemMission.ContainsKey(mission.missionName))
        {
            subscribeItemMission.Add(mission.missionName,mission);
            dialogueManager.StartDialogue(mission.conversions);
            StartCoroutine(CoStartMission(mission.detailName));
            mission.Init();
        }

        return true;
    }

    public bool StartEnemyMission(MissionEnemy mission)
    {
        if (!subscribeItemMission.ContainsKey(mission.missionName))
        {
            subscribeEnemyMission.Add(mission.missionName, mission);
            dialogueManager.StartDialogue(mission.conversions);
            StartCoroutine(CoStartMission(mission.detailName));
            mission.Init();
        }

        return true;
    }

    private IEnumerator CoStartMission(string detailName)
    {
        yield return new WaitUntil(() => !dialogueManager.GetIsDialogue());
        eventP.SetMovePlayer(true);
        uiToggleWindow.UpdateWindowText(detailName);
    }

    private IEnumerator CoStartComplete(string missionName)
    {
        yield return new WaitUntil(() => !dialogueManager.GetIsDialogue());
        eventP.SetMovePlayer(true);
        missionComplete.StartEffect(missionName);
        
    }

    public bool CheckMissionComplete(string missionName)
    {
        if(subscribeItemMission.ContainsKey(missionName)&&
           subscribeItemMission[missionName].condition.IsConditionMet())
        {
            eventP.DeleteItem(subscribeItemMission[missionName].condition.GetRequiredType(), subscribeItemMission[missionName].condition.GetCurrentAmount());
            dialogueManager.StartDialogue(subscribeItemMission[missionName].completeConversations);
            StartCoroutine(CoStartComplete(missionName));
            uiToggleWindow.DeleteWindowText(subscribeItemMission[missionName].detailName);
            return true;
        }

        return false;
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
