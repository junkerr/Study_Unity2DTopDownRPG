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
        talkData.Add(1000, new string[] { "照括?|0" });
        talkData.Add(2000, new string[] { "森焼肖|0", "せせせ|1", "せせせせせせせせ|2", "ししし?|0" });

        talkData.Add(100, new string[] { "汝骨廃 蟹巷雌切陥..", "..." });
        talkData.Add(200, new string[] { "..", "...雇短戚" });


        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "照括?|0", "戚 員拭澗 坦製戚姥蟹?|1", "陥献 原聖 紫寓級引 乞砧 企鉢 馬姥神慧|2" });
        talkData.Add(11 + 2000, new string[] { "格澗 戚 原聖 紫寓戚 焼艦姥蟹..|0", "戚 原聖拭澗 且虞錘 穿竺戚 赤走...|1", "照硝栗捜|2", "鎧 疑穿戚蟹 達焼神慧|3" });

        talkData.Add(20 + 1000, new string[] { "疑穿戚 嬢鋸劃姥??|0", "焼猿 原聖 焼掘楕拭辞 沙暗旭精汽..|1" });
        talkData.Add(20 + 2000, new string[] { "達紹嬢??|0" });
        talkData.Add(20 + 5000, new string[] { "疑穿聖 達紹陥!" });
        talkData.Add(21 + 2000, new string[] { "鎧 疑穿 鎧竃!|0" });



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

            //    // 背雁 締什闘 遭楳 授辞 企紫亜 蒸聖 凶
            //    // 締什闘 固 坦製 企紫研 亜走壱 身
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
            //    // 締什闘 固 坦製 企紫原煽 蒸聖 井酔
            //    // 奄沙 企紫研 亜走壱身
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
