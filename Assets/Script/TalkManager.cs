using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // Basic Talk
        talkData.Add(1000, new string[] { "�ȳ�?|0" });
        talkData.Add(2000, new string[] { "���ƾ�|0", "������|1", "����������������|2", "������?|0" });

        talkData.Add(100, new string[] { "����� �������ڴ�..", "..." });
        talkData.Add(200, new string[] { "..", "...��û��" });


        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "�ȳ�?|0", "�� ������ ó���̱���?|1", "�ٸ� ���� ������ ��� ��ȭ �ϱ�����|2" });
        talkData.Add(11 + 2000, new string[] { "�ʴ� �� ���� ����� �ƴϱ���..|0", "�� �������� ���� ������ ����...|1", "�Ⱦ˷���|2", "�� �����̳� ã�ƿ���|3" });

        talkData.Add(20 + 1000, new string[] { "������ ����ı�??|0", "�Ʊ� ���� �Ʒ��ʿ��� ���Ű�����..|1" });
        talkData.Add(20 + 2000, new string[] { "ã�Ҿ�??|0" });
        talkData.Add(20 + 5000, new string[] { "������ ã�Ҵ�!" });
        talkData.Add(21 + 2000, new string[] { "�� ���� ����!|0" });



        portraitData.Add(2000 + 0, portraitArr[0]);
        portraitData.Add(2000 + 1, portraitArr[1]);
        portraitData.Add(2000 + 2, portraitArr[2]);
        portraitData.Add(2000 + 3, portraitArr[3]);

        portraitData.Add(1000 + 0, portraitArr[4]);
        portraitData.Add(1000 + 1, portraitArr[5]);
        portraitData.Add(1000 + 2, portraitArr[6]);
        portraitData.Add(1000 + 3, portraitArr[7]);

    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            #region old
            //if (talkData.ContainsKey(id - id % 10))
            //{

            //    // �ش� ����Ʈ ���� ���� ��簡 ���� ��
            //    // ����Ʈ �� ó�� ��縦 ������ ��
            //    if (talkIndex == talkData[id - id % 10].Length)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        return talkData[id - id % 10][talkIndex];
            //    }

            //}
            //else
            //{
            //    // ����Ʈ �� ó�� ��縶�� ���� ���
            //    // �⺻ ��縦 �������
            //    if (talkIndex == talkData[id - id % 100].Length)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        return talkData[id - id % 100][talkIndex];
            //    }
            //}
            #endregion
            if (talkData.ContainsKey(id - 10 % 10))
            {
                return GetTalk(id - id % 10, talkIndex);
            }
            else
            {
                return GetTalk(id - id % 100, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
