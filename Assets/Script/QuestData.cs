using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcId; // ����Ʈ ��ȭ�� NPC�� ����

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
