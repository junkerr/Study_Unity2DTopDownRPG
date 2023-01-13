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
    public TextMeshProUGUI talkNpcName;

    public GameObject player;

    public TextMeshProUGUI questText;
    public TypeEffect talk;
    public GameObject scanObject;
    public GameObject menuSet;
    public Animator talkPanel;
    public Animator portaitAnim;
    public Image portraitImage;
    public int talkIndex;
    public bool isAction;

    public Sprite prevPortrait;
    private void Start()
    {
        GameLoad();
        isAction = false;
        //talkPanel.SetActive(false);
        questText.text = questManager.CheckQuest();
    }

    private void Update()
    {
        // Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
        }
    }

    public void SubMenuActive()
    {
        if (menuSet.activeSelf)
        {
            menuSet.SetActive(false);
        }
        else
        {
            menuSet.SetActive(true);
        }
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        string npcName = string.Empty;
        if(objData.isNpc)
        {
            npcName = scanObject.name;
        }

        Talk(objData.id, objData.isNpc, npcName);

        talkPanel.SetBool("isShow", isAction);

    }
    void Talk(int id, bool isNpc, string objectName)
    {
        talkNpcName.text = objectName;


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
            questText.text = questManager.CheckQuest(id);
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
                talkNpcName.text = "";
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

    public void GameSave()
    {
        // QuestID
        // QuestIndex
        // 캐릭터의 현재위치 
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionindex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        // 한번이라도 저장했는지 체크
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            int questId = PlayerPrefs.GetInt("QuestId");
            int questActionIndex = PlayerPrefs.GetInt("QuestActionindex");

            player.transform.position = new Vector3(x, y, 0);
            questManager.questId = questId;
            questManager.questActionIndex = questActionIndex;
            questManager.ControlObject();
        }

    }
    public void GameExit()
    {
        Application.Quit();
    }
}
