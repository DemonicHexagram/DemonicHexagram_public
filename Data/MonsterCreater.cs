using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterCreator : MonoBehaviour
{

    Enemy enemy;
    List<int> stageMonsterCodeList;

    int count = 0;

    List<int> _monsterActionParsing;
    List<Dictionary<string, object>> MonsterData;

    private void Awake()
    {
        MonsterData = CSVReader.Read(KeyWordManager.str_MonsterSheetTxt);
    }


    public void CreateMonster(List<int> MonsterCodeArray)
    {
        stageMonsterCodeList = MonsterCodeArray;
        count = 0;
        foreach(int monsterCode in stageMonsterCodeList)
        {
            GameObject EnemyObejct = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagEnemy);
            EnemyObejct.SetActive(true);
            enemy = EnemyObejct.GetComponent<Enemy>();
            
            enemy.enemyName = MonsterData[monsterCode][KeyWordManager.str_MonsterName].ToString();
            enemy.maxhp = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterMaxHp];
            enemy.minhp = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterMinHp];
            enemy.shieldPower = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterShieldPower];
            enemy.attack = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterAttack];
            enemy.elemental = (Elemental)MonsterData[monsterCode][KeyWordManager.str_MonsterElemental];
            string rawData = MonsterData[monsterCode][KeyWordManager.str_MonsterAction].ToString();
            _monsterActionParsing = rawData.Split(new char[] { KeyWordManager.char_UnderBar }).Select(n => Convert.ToInt32(n)).ToList();
            enemy.actions = _monsterActionParsing;
            
            enemy.effect = new int[3];
            enemy.effect[0] = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterWeak];
            enemy.effect[1] = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterStrength];
            enemy.effect[2] = (int)MonsterData[monsterCode][KeyWordManager.str_MonsterPoison];

            enemy.fullhp = Random.Range(enemy.minhp, enemy.maxhp);
            enemy.hp = enemy.fullhp;
            EnemyObejct.transform.position = 
                new Vector3(KeyWordManager.flt_DefaultLocationX+count* KeyWordManager.flt_LocationMultiplierX, KeyWordManager.flt_LocationMultiplierY,KeyWordManager.flt_LocationMultiplierZ + (count%2 == 1 ? -2:0));

            BattleManager.Instance.EnemyList.Add(enemy);
            count++;
        }

    }
}
