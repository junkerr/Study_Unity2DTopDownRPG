using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public GameObject talkPanel;
    public Image portraitImage;
    public int talkIndex;
    public bool isAction;

    private void Start()
    {
        isAction = false;
        talkPanel.SetActive(false);

        questManager.CheckQuest();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);

    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndexe(id);

        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData.Split("|")[0];

            portraitImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split("|")[1]));
            portraitImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraitImage.color = new Color(1, 1, 1, 0);

        }

        isAction = true;
        talkIndex++;
    }
}
