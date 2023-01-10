using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeEffect : MonoBehaviour
{
    private string targetMsg;
    public TextMeshProUGUI msgText;
    public GameObject EndCursor;

    AudioSource audio;
    // 1초에 몇글자씩
    public int CharPerSeconds;
    int index;

    public bool isAnimation;


    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audio = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAnimation)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        EndCursor.SetActive(false);

        msgText.text = "";
        index = 0;

        isAnimation = true;
        Invoke("Effecting", 1f / CharPerSeconds);
    }

    void Effecting()
    {

        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];

        // Sound
        if (targetMsg[index] != ' ' && targetMsg[index] != '.')
        {
            audio.Play();
        }

        index++;

        Invoke("Effecting", 1f / CharPerSeconds);
    }

    void EffectEnd()
    {
        isAnimation = false;
        EndCursor.SetActive(true);
    }

}
