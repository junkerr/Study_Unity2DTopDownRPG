using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    //public TextMeshProUGUI talkText;
    public TypeEffect talk;
    public GameObject scanObject;
    public Animator talkPanel;
    public Animator portaitAnim;
    public Image portraitImage;
    public int talkIndex;
    public bool isAction;

    public Sprite prevPortrait;

    private void Start()
    {
        isAction = false;
        //talkPanel.SetActive(false);

        questManager.CheckQuest();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow", isAction);

    }

    void Talk(int id, bool isNpc)
    {

        int questTalkIndex = 0;
        string talkData = "";
        if (talk.isAnimation)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndexe(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if (isNpc)
        {
            //talkText.text = talkData.Split("|")[0];
            talk.SetMsg(talkData.Split("|")[0]);

            portraitImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split("|")[1]));
            portraitImage.color = new Color(1, 1, 1, 1);

            // Animation Portrait
            if (prevPortrait != portraitImage)
            {
                portaitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImage.sprite;
            }

        }
        else
        {
            //talkText.text = talkData;
            talk.SetMsg(talkData);

            portraitImage.color = new Color(1, 1, 1, 0);

        }

        isAction = true;
        talkIndex++;
    }
}
