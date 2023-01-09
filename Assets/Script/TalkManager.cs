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
        talkData.Add(1000, new string[] { "안녕?|0" });
        talkData.Add(2000, new string[] { "예아앙|0", "ㅋㅋㅋ|1", "ㅋㅋㅋㅋㅋㅋㅋㅋ|2", "ㅇㅇㅇ?|0" });

        talkData.Add(100, new string[] { "평범한 나무상자다..", "..." });
        talkData.Add(200, new string[] { "..", "...멍청이" });


        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "안녕?|0", "이 곳에는 처음이구나?|1", "다른 마을 사람들과 모두 대화 하구오렴|2" });
        talkData.Add(11 + 2000, new string[] { "너는 이 마을 사람이 아니구나..|0", "이 마을에는 놀라운 전설이 있지...|1", "안알랴줌|2", "내 동전이나 찾아오렴|3" });

        talkData.Add(20 + 1000, new string[] { "동전이 어딨냐구??|0", "아까 마을 아래쪽에서 본거같은데..|1" });
        talkData.Add(20 + 2000, new string[] { "찾았어??|0" });
        talkData.Add(20 + 5000, new string[] { "동전을 찾았다!" });
        talkData.Add(21 + 2000, new string[] { "내 동전 내놔!|0" });



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

            //    // 해당 퀘스트 진행 순서 대사가 없을 때
            //    // 퀘스트 맨 처음 대사를 가지고 옴
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
            //    // 퀘스트 맨 처음 대사마저 없을 경우
            //    // 기본 대사를 가지고옴
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
