using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcId; // 퀘스트 대화할 NPC들 순번

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
