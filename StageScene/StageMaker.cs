using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{

    public MapManager mapManager;
    public List<Vector2Int> OutgoingNodePoints;
    private GameObject nodePrefab;
    bool IsCleared = false;
    public Vector3 _curPosition = Vector3.zero;

    public List<Node> nodes;


    public void StartMakeMap()
    {
        Vector2Int curPlayerPath;
        if (mapManager.CurrentMap.path.Count == 0)
        {
            curPlayerPath = Vector2Int.zero;
        }
        else
        {
            curPlayerPath = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
            IsCleared = true;
        }

        int curPathIdx;

        nodes = mapManager.CurrentMap.nodes;

        curPathIdx = nodes.FindIndex(node => node.point.Equals(curPlayerPath));
        if (curPathIdx < 0) curPathIdx = 0;
        SpawnNodes(mapManager.CurrentMap.nodes[curPathIdx], GameManager.Instance.MapNodeList[curPathIdx], GameManager.Instance.DataManager.PlayerTransformVector3-new Vector3(0,2,0),IsCleared);
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
        if(IsCleared) nodePrefab.transform.GetChild(0).gameObject.SetActive(false);
    }

}
