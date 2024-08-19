using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;


public class MapPlayerTracker : MonoBehaviour
{
    public bool lockAfterSelecting = false;
    public float enterNodeDelay = 1f;
    public MapManager mapManager;
    public MapView view;
    public static MapPlayerTracker Instance;
    public bool Locked { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool SelectNode(MapNode mapNode)
    {
        if (Locked) return false;

        if (mapManager.CurrentMap.path.Count == 0)
        {
            if (mapNode.Node.point.y == 0)
            {
                SendPlayerToNode(mapNode);
                return true;
            }
            else
            {
                PlayWarningThatNodeCannotBeAccessed();
                return false;
            }
        }
        else
        {
            Vector2Int currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
            Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

            if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
            {
                SendPlayerToNode(mapNode);
                return true;
            }
            else
            {
                PlayWarningThatNodeCannotBeAccessed();
                return false;
            }
        }
        
    }
    public bool CheckNode(MapNode mapNode)
    {
        if (Locked) return false;

        if (mapManager.CurrentMap.path.Count == 0)
        {
            if (mapNode.Node.point.y == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Vector2Int currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
            Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

            if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    private void SendPlayerToNode(MapNode mapNode)
    {
        Locked = lockAfterSelecting;
        mapManager.CurrentMap.path.Add(mapNode.Node.point);
        mapManager.SaveMap();
        view.SetAttainableNodes();
        view.SetLineColors();
        mapNode.ShowSwirlAnimation();

        DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
    }

    private static void EnterNode(MapNode mapNode)
    {
        switch (mapNode.Node.nodeType)
        {
            case NodeType.MinorEnemy:
                break;
            case NodeType.EliteEnemy:
                break;
            case NodeType.RestSite:
                break;
            case NodeType.Store:
                break;
            case NodeType.Boss:
                break;
            case NodeType.Mystery:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlayWarningThatNodeCannotBeAccessed()
    {
        Debug.Log("Selected node cannot be accessed");
    }
}