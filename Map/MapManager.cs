using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;
    private string filePath;
    public bool isGameLoaded;
    public List<Node> nodes;
    bool IsCleared = false;
    GameObject nodePrefab;
    public Map CurrentMap { get; private set; }

    private void Start()
    {
        GameManager.Instance.mapManager = this;
        filePath = $"{Application.dataPath}/Resources/Map.csv";
        isGameLoaded = !GameManager.Instance.isNewGame;
        if (System.IO.File.Exists(filePath) && isGameLoaded == true)
        {
            var csv = System.IO.File.ReadAllText(filePath);
            Map map = Map.FromCsv(csv);
            if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            {
                GenerateNewMap();
                GameManager.Instance.isNewGame = false;
            }
            else
            {
                CurrentMap = map;
                view.ShowMap(map);
            }
        }
        else
        {
            GenerateNewMap();
            GameManager.Instance.isNewGame = false;

        }
        StartMakeMap();
        SaveMap();
    }

    public void GenerateNewMap()
    {
        Map map = MapGenerator.GetMap(config);
        CurrentMap = map;
        view.ShowMap(map);
    }

    public void SaveMap()
    {
        if (CurrentMap == null) return;

        var csv = CurrentMap.ToCsv();
        System.IO.File.WriteAllText(filePath, csv);
    }


    public void StartMakeMap()
    {
        Vector2Int curPlayerPath;
        if (CurrentMap.path.Count == 0)
        {
            curPlayerPath = Vector2Int.zero;
        }
        else
        {
            curPlayerPath = CurrentMap.path[CurrentMap.path.Count - 1];
            IsCleared = true;
        }

        int curPathIdx;

        nodes = CurrentMap.nodes;

        curPathIdx = nodes.FindIndex(node => node.point.Equals(curPlayerPath));
        if (curPathIdx < 0) curPathIdx = 0;
        SpawnNodes(CurrentMap.nodes[curPathIdx], GameManager.Instance.MapNodeList[curPathIdx], GameManager.Instance.DataManager.PlayerTransformVector3 - new Vector3(0, 2, 0), IsCleared);
        SaveMap();
    }

    private void SpawnNodes(Node node, MapNode mapNode, Vector3 positionVector3, bool IsCleared)
    {
        switch (node.nodeType)
        {
            case NodeType.MinorEnemy:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagMinorEnemyBlock);
                break;
            case NodeType.EliteEnemy:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagEliteEnemyBlock);
                break;
            case NodeType.Mystery:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagMysteryBlock);
                break;
            case NodeType.Boss:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagBossBlock);
                break;
            case NodeType.Store:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagStoreBlock);
                break;
            case NodeType.RestSite:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagRestSiteBlock);
                break;
        }
        nodePrefab.transform.position = positionVector3;
        nodePrefab.GetComponent<BlockProperties>().Node = node;
        nodePrefab.GetComponent<BlockProperties>().Mapnode = mapNode;
        if (IsCleared) nodePrefab.transform.GetChild(0).gameObject.SetActive(false);

    }
}