using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBlockController : MonoBehaviour
{
    public BlockProperties BlockProperties;
    public NodeBlueprint NodeBlueprint;
    public GameObject Wall;
    public GameObject PassWall;
    public GameObject StoreUI;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (MapPlayerTracker.Instance.CheckNode(BlockProperties.Mapnode))
    //    {
    //        Wall.SetActive(true);
    //    }
    //    else
    //    {
    //        PassWall.SetActive(true);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F))//상호작용 시 행동
        {
            if (MapPlayerTracker.Instance.SelectNode(BlockProperties.Mapnode))
            {
                switch (NodeBlueprint.nodeType)
                {
                    case NodeType.MinorEnemy:
                        GameManager.Instance.DataManager.CountingData.MapAddCounting();
                        SceneManager.LoadScene(KeyWordManager.str_BattleSceneTxt);
                        SoundManager.Instance.PlayBgm((int)ClipList.Battle3);
                        break;
                    case NodeType.EliteEnemy:
                        GameManager.Instance.DataManager.CountingData.MapAddCounting();
                        SceneManager.LoadScene(KeyWordManager.str_BattleSceneTxt);
                        SoundManager.Instance.PlayBgm((int)ClipList.Battle3);
                        break;
                    case NodeType.RestSite:
                        GameManager.Instance.Player.ChangeCanMove(false);
                        GameManager.Instance.DataManager.CountingData.RestAddCounting();
                        GameManager.Instance.DataManager.CountingData.MapAddCounting();
                        StageUIManager.Instance.OnRestUI();
                        break;
                    case NodeType.Store:
                        GameManager.Instance.DataManager.CountingData.StoreAddCounting();
                        GameManager.Instance.DataManager.CountingData.MapAddCounting();
                        GameManager.Instance.Player.ChangeCanMove(false);
                        StageUIManager.Instance.OnStoreUIOn();
                        break;
                    case NodeType.Boss:
                        GameManager.Instance.DataManager.CountingData.BossAddCounting();
                        GameManager.Instance.DataManager.CountingData.MapAddCounting();
                        SceneManager.LoadScene(KeyWordManager.str_BossBattleSceneTxt);
                        SoundManager.Instance.PlayBgm((int)ClipList.Battle2);
                        break;
                    case NodeType.Mystery:
                        GameManager.Instance.DataManager.CountingData.MysteryAddCounting();
                        GameManager.Instance.DataManager.CountingData.MapAddCounting();
                        GameManager.Instance.Player.ChangeCanMove(false);
                        Debug.Log("사건 시작");
                        GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagIncidentScene);
                        break;
                    default:
                        break;
                }
                PassWall.SetActive(true);
                Wall.SetActive(false);
            }


        }
    }
    private void OnDisable()
    {
        if (Wall != null) Wall.SetActive(false);
        if (PassWall != null) PassWall.SetActive(false);
    }
}
//참고용
//public enum NodeType
//{
//    MinorEnemy,
//    EliteEnemy,
//    RestSite,
//    Store,
//    Boss,
//    Mystery
//}

