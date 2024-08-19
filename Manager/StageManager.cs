using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    MonsterCreator monsterCreator;
    List<Dictionary<string, object>> StageData;
    List<int> _monsterListArray;
    private void Start()
    {
        monsterCreator = GetComponent<MonsterCreator>();
        StageData = CSVReader.Read(KeyWordManager.str_StageSheetTxt);

        StageInfo(UnityEngine.Random.Range(0, 9));
    }

    public void StageInfo(int stageNum)
    {
        string rawData = StageData[stageNum]["monster"].ToString();
        _monsterListArray = rawData.Split(new char[] { '.' }).Select(n => System.Convert.ToInt32(n)).ToList();
        monsterCreator.CreateMonster(_monsterListArray);
    }
}
 