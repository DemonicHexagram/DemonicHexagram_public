using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockLifeCycleController : MonoBehaviour
{
    public Node thisNode;
    private GameObject nodePrefab;
    public GameObject ThisGameObject;
    private List<int> OutGoingNodeIndex = new List<int>();

    public MapNode thisMapNode;

    public BlockProperties properties;
    private TwoWayBlockWallControl twoWayBlockWallControl;

    private int count = 0;

    public GameObject Wall;
    public GameObject PassWall;


    private void OnTriggerEnter(Collider other)
    {
        thisNode = properties.Node;
        if (GameManager.Instance.CreatedBlockList != null)
        {
            foreach (GameObject go in GameManager.Instance.CreatedBlockList)
            {
                if (go.GetComponent<BlockProperties>() != properties)
                    go.SetActive(false);
            }
            GameManager.Instance.CreatedBlockList.Clear();
        }

        switch (thisNode.outgoing.Count)
        {
            case 0:
            case 1:
            case 2:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagTwoWayStageNode);
                break;
            case 3:
                nodePrefab = GameManager.Instance.ObjectPool.SpawnFromPool("ThreeWayStageNode");
                break;
            default:
                break;
        }
        nodePrefab.transform.position = this.transform.position + new Vector3(20, 0, 0);
        twoWayBlockWallControl = nodePrefab.GetComponent<TwoWayBlockWallControl>();

        twoWayBlockWallControl.EnableTheWalls(properties.Node.outgoing.Count);

        foreach (Vector2Int outgoingVector2 in properties.Node.outgoing)
        {
            OutGoingNodeIndex.Add(GameManager.Instance.NodeList.FindIndex(item => item.point.Equals(outgoingVector2)));
        }
        if (OutGoingNodeIndex != null)
        {
            OutGoingNodeIndex.Reverse();
            foreach (int outgoingNode in OutGoingNodeIndex)
            {
                count++;
                nodePrefab = SpawnChildNode(GameManager.Instance.NodeList[outgoingNode], GameManager.Instance.MapNodeList[outgoingNode]);
                nodePrefab.transform.position = ThisGameObject.transform.position + new Vector3(5 + (count == 1 ? 35 : 0)+((count == 2) ? 30 : 0) + (count == 3 ? 35 : 0), (count == 3 ? 8 : 0), (count == 2 ? 10 : 0));//새블럭변경,디버깅용
            }
            count = 0;
        }

        if (MapPlayerTracker.Instance.CheckNode(properties.Mapnode))
        {
            Wall.SetActive(true);
        }
        else
        {
            PassWall.SetActive(true);
        }

    }

    private GameObject SpawnChildNode(Node node, MapNode mapNode)
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
        nodePrefab.GetComponent<BlockProperties>().Node = node;
        nodePrefab.GetComponent<BlockProperties>().Mapnode = mapNode;

        GameManager.Instance.CreatedBlockList.Add(nodePrefab);

        return nodePrefab;
    }

    private void OnTriggerExit(Collider other)
    {
        ThisGameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (Wall != null) Wall.SetActive(false);
        if (PassWall != null) PassWall.SetActive(false);
    }
   
}
