using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreater : MonoBehaviour
{
    public Hexaghost Boss;

    public void Start()
    {
        GameObject EnemyObejct = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagEnemy);
        EnemyObejct.SetActive(true);
        Boss = EnemyObejct.GetComponent<Hexaghost>();
        BattleManager.Instance.EnemyList.Add(Boss);

        Boss.maxhp = 250;
        Boss.minhp = 250;
        Boss.hp = 250;
        Boss.fullhp = Boss.hp;
        Boss.enemyName = "육각령";
        Boss.shieldPower = 10;
        Boss.attack = 7;
        Boss.actions = new List<int> { 0, 1, 4, 5, 6, 3};
        Boss.elemental = Elemental.None;

        Boss.effect = new int[3];
        Boss.effect[0] = 0;
        Boss.effect[1] = 0;
        Boss.effect[2] = 3;
    }
}
